namespace l99.fanuc.MyTasks;

public class ParameterDump
{
    private readonly ILogger _logger;
    
    public ParameterDump()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var platform = new Platform(stoppingToken);
        
        _logger.Info("connect to machine...");
        var connection = await platform.ConnectAsync("localhost", 8193, 3);
        
        var paranum = await platform.RdParaNumAsync();
        var paranum_inner = paranum.response.cnc_rdparanum.paranum;

        Console.WriteLine($"Minimum: {paranum_inner.para_min}, " +
                          $"Maximum: {paranum_inner.para_max}, " +
                          $"Total:{paranum_inner.total_no}");

        var all_done = false;
        var start = (short) paranum_inner.para_min;

        while (!all_done)
        {
            var parainfo = await platform.RdParaInfoAsync(start, 10);
            var parainfo_inner = parainfo.response.cnc_rdparainfo.paraif;

            if (parainfo_inner.next_no < start)
                all_done = true;

            start = parainfo_inner.next_no;

            var fields = parainfo_inner.info.GetType().GetFields();

            for (var i = 0; i <= parainfo_inner.info_no - 1; i++)
            {
                var value = fields[i].GetValue(parainfo_inner.info);

                Console.WriteLine($"Param: {value.prm_no}, Type: {value.prm_type}");
            }
        }

        Console.Write("*** *** **** ***");
        
        _logger.Info("disconnect from machine...");
        var disconnection = await platform.DisconnectAsync();
    }
}