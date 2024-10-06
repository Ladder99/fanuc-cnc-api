namespace l99.fanuc;

public class MyCode
{
    private readonly ILogger _logger;

    public MyCode()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    public async Task RunAsync(CancellationToken stoppingToken)
    {
        _logger.Info("my code starting");
        
        try
        {
            // var task = new MyTasks.ParameterDump();
            // var task = new MyTasks.ProgramUpload();
            var task = new MyTasks.ListAxesAndSpindles();
            await task.ExecuteAsync(stoppingToken);
        }
        catch (OperationCanceledException)
        {
            // When the stopping token is canceled, for example, a call made from services.msc,
            // we shouldn't exit with a non-zero exit code. In other words, this is expected...
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "{Message}", ex.Message);

            // Terminates this process and returns an exit code to the operating system.
            // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
            // performs one of two scenarios:
            // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
            // 2. When set to "StopHost": will cleanly stop the host, and log errors.
            //
            // In order for the Windows Service Management system to leverage configured
            // recovery options, we need to terminate the process with a non-zero exit code.
            Environment.Exit(1);
        }
        
        _logger.Info("my code finished");
    }
}