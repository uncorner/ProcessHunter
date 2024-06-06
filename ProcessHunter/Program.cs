using System.Diagnostics;

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
    //await Task.Delay(timeout);
}

//Console.WriteLine("\nPress enter...");
//Console.ReadLine();
