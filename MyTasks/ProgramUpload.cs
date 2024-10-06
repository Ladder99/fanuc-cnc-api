namespace l99.fanuc.MyTasks;

public class ProgramUpload
{
    private readonly ILogger _logger;
    
    public ProgramUpload()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }
    
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var platform = new Platform(stoppingToken);
        
        _logger.Info("connect to machine...");
        var connection = await platform.ConnectAsync("localhost", 8193, 3);
        
        string program_contents = "";

        var start = await platform.UpStart3Async(0, 1, 1);

        if (Focas.focas_ret.EW_OK == start.rc)
        {
            var upload = await platform.Upload3Async(256);

            while (Focas.focas_ret.EW_OK == upload.rc || Focas.focas_ret.EW_BUFFER == upload.rc)
            {
                if (Focas.focas_ret.EW_OK == upload.rc)
                {
                    program_contents += new string(upload.response.cnc_upload3.data.data);
                    program_contents = program_contents.Replace("\0", string.Empty);
                }

                if (program_contents.Length > 0 &&
                    program_contents.Substring(program_contents.Length - 1, 1) == "%")
                {
                    var end = await platform.UpEnd3Async();
                    break;
                }

                upload = await platform.Upload3Async(256);
            }
        }
        
        _logger.Info(program_contents);
        
        _logger.Info("disconnect from machine...");
        var disconnection = await platform.DisconnectAsync();
    }
}