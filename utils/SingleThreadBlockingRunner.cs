// ReSharper disable once CheckNamespace
namespace l99.fanuc.utils;

// https://stackoverflow.com/questions/61530632/forcing-certain-code-to-always-run-on-the-same-thread
public class SingleThreadBlockingRunner
{
    public bool IsRunning { get; private set; }
    private CancellationToken _stoppingToken;
    
    private readonly SemaphoreSlim _semaphore;
    private readonly ManualResetEventSlim _newTaskRunSignal;

    private TaskCompletionSource<object> _taskCompletionSource = null!;
    private Func<object> _func = null!;

    public SingleThreadBlockingRunner(CancellationToken stoppingToken)
    {
        IsRunning = true;
        _stoppingToken = stoppingToken;
        _semaphore = new SemaphoreSlim(1, 1);
        _newTaskRunSignal = new ManualResetEventSlim(false);
        var contextThread = new Thread(ThreadLooper)
        {
            Priority = ThreadPriority.Highest
        };
        contextThread.Start();
    }

    private void ThreadLooper()
    {
        while (true)
        {
            //wait till the next task signal is received.
            // even though MRE allows multiple threads to pass, the outer semaphore disallows this behavior
            try
            {
                _newTaskRunSignal.Wait(_stoppingToken);
            }
            catch (OperationCanceledException oce)
            {
                // intercept cancel request and kill thread for clean shutdown
                break;
            }

            _newTaskRunSignal.Reset();

            //next task execution signal is received.
            try
            {
                //try execute the task and get the result
                var result = _func.Invoke();

                //task executed successfully, set the result
                _taskCompletionSource.SetResult(result);
            }
            catch (Exception ex)
            {
                //task execution threw an exception, set the exception and continue with the looper
                _taskCompletionSource.SetException(ex);
            }
        }

        IsRunning = false;
    }

    public async Task<TResult> Run<TResult>(Func<TResult> func, CancellationToken cancellationToken = default(CancellationToken))
    {
        //allows only one thread to run at a time.
        await _semaphore.WaitAsync(cancellationToken);

        //thread has acquired the semaphore and entered
        try
        {
            //create new task completion source to wait for func to get executed on the context thread
            _taskCompletionSource = new TaskCompletionSource<object>();

            //set the function to be executed by the context thread
            _func = () => func()!;

            //signal the waiting context thread that it is time to execute the task
            _newTaskRunSignal.Set();

            //wait and return the result till the task execution is finished on the context/looper thread.
            return (TResult)await _taskCompletionSource.Task;
        }
        finally
        {
            //release the semaphore to allow other threads to acquire it.
            _semaphore.Release();
        }
    }
}