using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProcessHunter;

public class Worker(ILogger<Worker> logger, IConfiguration config)
    : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timeout = config.GetCheckingTimeout();
        var mainProcName = config.GetMainProcessName();
        var unwantedProcName = config.GetUnwantedProcessName();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var wgProcess = Process.GetProcessesByName(mainProcName);
            if (wgProcess.Length > 0)
            {
                logger.LogInformation($"Main process '{mainProcName}' found. Looking for unwanted process...");

                var processes = Process.GetProcessesByName(unwantedProcName);
                if (processes.Length == 0)
                {
                    logger.LogInformation("Unwanted process not found");
                }
                else
                {
                    foreach (var proc in processes)
                    {
                        logger.LogInformation($"Unwanted process '{unwantedProcName}' found: ID {proc.Id}");
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
