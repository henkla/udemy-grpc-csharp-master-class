using Grpc.Core;
using Sqrt;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SquareRootAPI.Client
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50055;

        static async Task Main(string[] args)
        {
            // connection
            var channel = new Channel($"{_host}:{_port}", ChannelCredentials.Insecure);

            // connect
            await channel.ConnectAsync().ContinueWith((task) => 
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine($"Successfully connected to {_host}:{_port}");
            });

            // client
            var client = new SqrtService.SqrtServiceClient(channel);

            try
            {
                // send request
                var number = 15;
                var response = await client.SqrtAsync(new SqrtRequest() { Number = number });
                
                // read response
                Console.WriteLine($"Square root of {number} is {response.SquareRoot}");
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.InvalidArgument)
            {
                Console.WriteLine($"Invalid argument: {e.Status.Detail}");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"An error occured: {e.Message}");
            }

            // shutdown
            Console.ReadKey();
            await channel.ShutdownAsync();
        }
    }
}
