using Compute;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeAverageAPI.Client
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50053;

        static async Task Main(string[] args)
        {
            // this is the connection to the endpoint (the server)
            var channel = new Channel($"{_host}:{_port}", ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"Successfully connected to {_host}:{_port}");
                }
            });

            // we also need a client to handle the request to the server
            var client = new ComputeAverageService.ComputeAverageServiceClient(channel);

            // this is the data that is going to be sent to the server
            var numbers = new int[] { 2, 3, 4, 5, 6, 7 };

            // create the response stream (a place to store the reply that is going to be streamed)
            var responseStream = client.LongCompute();

            // this is where we send a stream of requests to the server using the client above
            foreach (var number in numbers)
            {
                Console.WriteLine($"Sending request with value {number}");
                await responseStream.RequestStream.WriteAsync(new LongComputeRequest() { Value = number });
                await Task.Delay(1000);
            }

            // complete the request
            await responseStream.RequestStream.CompleteAsync();

            // get the response from the response stream above
            var response = await responseStream.ResponseAsync;
            Console.WriteLine($"Response: The average value of {CollectionToString(numbers)} (={numbers.Sum()}) is {response.Result}");

            // shut down the connection to the server
            Console.ReadKey();
            channel.ShutdownAsync().Wait();
        }

        private static string CollectionToString<T>(IEnumerable<T> collection)
        {
            var sb = new StringBuilder("{ ");
            
            foreach (var item in collection) 
            {
                sb.Append($"\"{item}\"");

                if (!item.Equals(collection.Last()))
                {
                    sb.Append(", ");
                }
                else
                {
                    sb.Append(" }");
                }
            }

            return sb.ToString();
        }
    }
}
