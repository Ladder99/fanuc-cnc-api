using NLog.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace l99.fanuc;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        
        var builder = new HostApplicationBuilder();

        if (WindowsServiceHelpers.IsWindowsService())
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "fanuc-cnc-api";
            });

        builder.Services.AddSingleton(args);
        builder.Services.AddHostedService<FanucService>();
        
        IHost host = builder.Build();
        await host.RunAsync();
    }
}

public class FanucService : BackgroundService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly string[] _args;

    public FanucService(IHostApplicationLifetime lifetime, string[] args)
    {
        _lifetime = lifetime;
        _args = args;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Application starting...");
        Console.WriteLine($"Bitness: {(IntPtr.Size == 8 ? "64-bit" : "32-bit")}");
        LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
        var config = new ConfigurationBuilder().Build();
        LogManager.Setup().SetupExtensions(ext => ext.RegisterConfigSettings(config));
        return base.StartAsync(cancellationToken);
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await new MyCode().RunAsync(stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Application stopping...");
        LogManager.Shutdown();
        return base.StopAsync(cancellationToken);
    }
}