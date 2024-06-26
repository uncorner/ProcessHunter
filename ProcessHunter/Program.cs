using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessHunter;

//namespace ProcessHunter;

/*
const string mainProcName = "wg-crypt-uncorn";
//const string mainProcName = "gnome-power-statistics";
//var unwantedProcName = "gnome-calculator";
const string unwantedProcName = "qbittorrent";
const int timeout = 7 * 1000;

while (true)
{
    var wgProcess = Process.GetProcessesByName(mainProcName);
    if (wgProcess.Length > 0)
    {
        Console.WriteLine($"Main process found. Looking for unwanted process...");

        var processes = Process.GetProcessesByName(unwantedProcName);
        if (processes.Length == 0)
        {
            Console.WriteLine("Unwanted process not found");
        }
        else
        {
            foreach (var proc in processes)
            {
                Console.WriteLine($"Unwanted process found: ID {proc.Id}");
                Console.WriteLine($"kill process with ID {proc.Id}");
                proc.Kill(true);
            }
        }
    }
    else
    {
        Console.WriteLine($"Main process not found. Do nothing");
    }
    
    Console.WriteLine($"Waiting for timeout");
    await Task.Delay(timeout);
}

//Console.WriteLine("\nPress enter...");
//Console.ReadLine();
*/

// using IHost host = Host.CreateApplicationBuilder(args).Build();
//
// // Ask the service provider for the configuration abstraction.
// IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
//
// // Get values from the config given their key and their target type.
// int keyOneValue = config.GetValue<int>("KeyOne");
// bool keyTwoValue = config.GetValue<bool>("KeyTwo");
// string? keyThreeNestedValue = config.GetValue<string>("KeyThree:Message");
//
// // Write the values to the console.
// Console.WriteLine($"KeyOne = {keyOneValue}");
// Console.WriteLine($"KeyTwo = {keyTwoValue}");
// Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");
//
// // Application code which might rely on the config could start here.
//
// await host.RunAsync();


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

