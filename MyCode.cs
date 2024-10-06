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
        var platform = new Platform(stoppingToken);

        while(!stoppingToken.IsCancellationRequested)
        {
            _logger.Info("connect to machine...");
            var connection = await platform.ConnectAsync("localhost", 8193, 10);
            _logger.Info("read system info...");
            var sysinfo = await platform.SysInfoAsync();
            _logger.Info("disconnect from machine...");
            var disconnection = await platform.DisconnectAsync();
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}