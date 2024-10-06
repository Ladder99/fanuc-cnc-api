namespace l99.fanuc.MyTasks;

public class ListAxesAndSpindles
{
    private readonly ILogger _logger;
    
    public ListAxesAndSpindles()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var platform = new Platform(stoppingToken);
            
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.Info("connect to machine...");
            var connection = await platform.ConnectAsync("localhost", 8193, 3);
            
            var paths = await platform.GetPathAsync();
            var max_path = paths.response.cnc_getpath.maxpath_no;

            for (short i = 0; i <= max_path; i++)
            {
                var path = await platform.SetPathAsync(i);
                
                var axes = await platform.RdAxisNameAsync();
                var spindles = await platform.RdSpdlNameAsync();
            }
            
            _logger.Info("disconnect from machine...");
            var disconnection = await platform.DisconnectAsync();
                
            await Task.Delay(1000, stoppingToken);
        }
    }
}