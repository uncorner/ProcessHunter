using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProcessHunter;

public class Worker(ILogger<Worker> logger, IConfiguration config)
    : BackgroundService
{
    //private ILogger<Worker> logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (logger.IsEnabled(LogLevel.Information))
        //     {
        //         logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     }
        //     await Task.Delay(1000, stoppingToken);
        // }
        
        //const string mainProcName = "wg-crypt-uncorn";
        //const string mainProcName = "gnome-power-statistics";
        //var unwantedProcName = "gnome-calculator";
        //const string unwantedProcName = "qbittorrent";
        //const int timeout = 7 * 1000;
        
        var timeout = config.GetCheckingTimeout();
        var mainProcName = config.GetMainProcessName();
        var unwantedProcName = config.GetUnwantedProcessName();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var wgProcess = Process.GetProcessesByName(mainProcName);
            if (wgProcess.Length > 0)
            {
                //Console.WriteLine($"Main process found. Looking for unwanted process...");
                logger.LogInformation($"Main process '{mainProcName}' found. Looking for unwanted process...");

                var processes = Process.GetProcessesByName(unwantedProcName);
                if (processes.Length == 0)
                {
                    //Console.WriteLine("Unwanted process not found");
                    logger.LogInformation("Unwanted process not found");
                }
                else
                {
                    foreach (var proc in processes)
                    {
                        //Console.WriteLine($"Unwanted process found: ID {proc.Id}");
                        logger.LogInformation($"Unwanted process '{unwantedProcName}' found: ID {proc.Id}");
                        //Console.WriteLine($"kill process with ID {proc.Id}");
                        logger.LogInformation($"kill process with ID {proc.Id}");
                        proc.Kill(true);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Main process not found. Do nothing");
            }
    
            Console.WriteLine($"Waiting for timeout");
            await Task.Delay(timeout, stoppingToken);
        }
        
    }
}
