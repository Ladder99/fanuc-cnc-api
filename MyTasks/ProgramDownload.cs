namespace l99.fanuc.MyTasks;

public class ProgramDownload
{
    private readonly ILogger _logger;
    
    public ProgramDownload()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var program = new StringBuilder();
        program.AppendLine("");
        program.AppendLine("01234");
        program.AppendLine("(**W/O:W62482**************)");
        program.AppendLine("(**LOT:1*******************)");
        program.AppendLine("(**OP:40*******************)");
        program.AppendLine("(**P/N:40905***************)");
        program.AppendLine("(**QTY REQ:4000************)");
        program.AppendLine("(**RATE:3******************)");
        program.AppendLine("(*** AUTO CYCLE PROGRAM ***)");
        program.AppendLine("N5");
        program.AppendLine("(*** PALLET CHANGE ***)");
        program.AppendLine("G91G28X0Y0Z0");
        program.AppendLine("G91G28B0");
        program.AppendLine("%");
        
        var platform = new Platform(stoppingToken);
        
        _logger.Info("connect to machine...");
        var connection = await platform.ConnectAsync("localhost", 8193, 3);
        
        _logger.Info("DOWNLOAD START");
        var start = await platform.DwnStart3Async(0);

        if (Focas.focas_ret.EW_OK == start.rc)
        {
            var lines = program.ToString().Split("\r\n");

            int line_index = 0;
            
            var download = await platform.Download3Async(lines[line_index].Length+1, (lines[line_index]+"\n").ToCharArray());

            while (Focas.focas_ret.EW_OK == download.rc || Focas.focas_ret.EW_BUFFER == download.rc)
            {
                _logger.Info($"{(download.rc == Focas.EW_OK ? "SENT" : "NOT_SENT")}  {lines[line_index]}");
                
                if (line_index == lines.Length-1)
                {
                    var end = await platform.DwnEnd3Async();
                    _logger.Info("DOWNLOAD END");
                    break;
                }
                
                if (Focas.focas_ret.EW_OK == download.rc)
                {
                    line_index += 1;
                }
                
                download = await platform.Download3Async(lines[line_index].Length+1, (lines[line_index]+"\n").ToCharArray());
            }
        }
        
        //_logger.Info(program_contents);
        
        _logger.Info("disconnect from machine...");
        var disconnection = await platform.DisconnectAsync();
    }
}