using System;
using System.Net;
using System.Threading;

namespace BB.ScalingAzureFunctions.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri apiUri = new Uri(args[0]);

            ServicePointManager.DefaultConnectionLimit = 100;
            Console.WriteLine($"Connection limit: {ServicePointManager.DefaultConnectionLimit}");

            while (true)
            {
                using (WebClient webClient = new WebClient())
                {
                    Console.WriteLine($"Creating request @ {DateTime.Now}");
                    webClient.DownloadStringAsync(apiUri);
                }

                Thread.Sleep(20);
            }
        }
    }
}

