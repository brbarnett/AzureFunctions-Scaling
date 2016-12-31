using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
                Console.WriteLine($"Creating request @ {DateTime.Now}");
                Task.Run(async () =>
                {
                    using (WebClient webClient = new WebClient())
                    {
                        Console.WriteLine(await webClient.DownloadStringTaskAsync(apiUri));
                    }
                });

                Thread.Sleep(20);
            }
        }
    }
}

