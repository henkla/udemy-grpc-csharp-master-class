using Grpc.Core;
using Prime;
using System;
using System.Threading.Tasks;

namespace PrimeNumberDecompositionAPI.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // we need the actual connection
            var channel = new Channel("localhost:50052", ChannelCredentials.Insecure);

            // try to connect to the server using the channel
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected to the server successfully");
                }
            });

            // we need the actual client and to provide it with the channel/connection
            var primeDecompositionClient = new PrimeNumberDecompositionService.PrimeNumberDecompositionServiceClient(channel);

            // this is the actual request data to be sent to the server (using the client)
            var number = 120;
            var primeDecompositionRequest = new PrimeNumberDecompositionRequest { Number = number };

            // use the client to send the request to the server
            Console.WriteLine($"Sending the following request: {primeDecompositionRequest}");
            var primeDecompositionResponse = primeDecompositionClient.Decompose(primeDecompositionRequest);

            // extract the (stream of) response from the server
            Console.WriteLine($"The prime number decomposition of {120} is:");
            while (await primeDecompositionResponse.ResponseStream.MoveNext())
            {
                Console.WriteLine($"- {primeDecompositionResponse.ResponseStream.Current.PrimeNumber}");
            }

            // shut down the connection to the server
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
