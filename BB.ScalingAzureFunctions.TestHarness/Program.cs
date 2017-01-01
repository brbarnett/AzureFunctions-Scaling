using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BB.ScalingAzureFunctions.TestHarness
{
    class Program
    {
        private static readonly Random RandomInstance = new Random();

        static void Main(string[] args)
        {   
            Console.WriteLine($"Connection limit: {ServicePointManager.DefaultConnectionLimit}");

            while (true)
            {
                Console.WriteLine($"Creating request @ {DateTime.Now}");
                Task.Run(async () => await TestUpdate());

                Thread.Sleep(20);   // 1000 / 50 == 20 for 50 req/sec
            }
        }

        private static async Task TestGet()
        {
            int id = RandomInstance.Next(1, 100);
            Console.WriteLine(await Request($"http://bb-scaling-api.azurewebsites.net/api/people?id={id}", "GET"));
        }

        private static async Task TestUpdate()
        {
            int id = RandomInstance.Next(1, 100);
            Console.WriteLine(await Request($"http://bb-scaling-api.azurewebsites.net/api/people?id={id}&lastUpdated={DateTime.Now}", "PATCH"));
        }

        private static async Task<string> Request(string url, string method)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;

            string responseContent = string.Empty;

            using (WebResponse response = await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader sr99 = new StreamReader(stream))
            {
                responseContent = await sr99.ReadToEndAsync();
            }

            return responseContent;
        }
    }
}

