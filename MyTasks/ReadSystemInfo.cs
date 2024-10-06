namespace l99.fanuc.MyTasks;

public class ReadSystemInfo
{
    private readonly ILogger _logger;
    
    public ReadSystemInfo()
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
            _logger.Info("read system info...");
            var sysinfo = await platform.SysInfoAsync();
            _logger.Info("disconnect from machine...");
            var disconnection = await platform.DisconnectAsync();
                
            await Task.Delay(1000, stoppingToken);
        }
    }
}