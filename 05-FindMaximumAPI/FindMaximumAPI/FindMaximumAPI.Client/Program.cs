using Calculator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindMaximumAPI.Client
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50054;

        static async Task Main(string[] args)
        {
            // this is the actual connection
            var channel = new Channel($"{_host}:{_port}", ChannelCredentials.Insecure);
            
            // try to open up the connection
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"Successfully connected to {_host}:{_port}");
                }
            });

            // we need a client to actually handle the requests towards the server
            var client = new CalculatorService.CalculatorServiceClient(channel);

            // we need a stream to store the response in
            var stream = client.FindMaximum();

            // set up the task where we will actually recieve the server responses in
            var responseReaderTask = Task.Run(async () => 
            { 
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"Current maximum: {stream.ResponseStream.Current.Result}");
                }
            });

            // this will be the numbers to send to the service
            var numbers = new List<int>() { 1, 5, 3, 6, 2, 20 };

            foreach (var number in numbers)
            {
                Console.WriteLine($"Sending request: {number}");
                await stream.RequestStream.WriteAsync(new FindMaximumRequest { Number = number });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;

            // shut down the connection to the server
            Console.ReadKey();
            channel.ShutdownAsync().Wait();
        }
    }
}
