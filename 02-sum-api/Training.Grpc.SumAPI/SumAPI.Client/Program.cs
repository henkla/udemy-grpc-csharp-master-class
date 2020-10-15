using Grpc.Core;
using Sum;
using System;

namespace SumAPI.Client
{
    class Program
    {
        private const string _target = "localhost:50051";

        static void Main(string[] args)
        {
            // we need a channel - the actual connection wiring
            var channel = new Channel(_target, ChannelCredentials.Insecure);
            
            // check to see if the connection method ran to completion
            // if so, the connection was successful
            channel.ConnectAsync().ContinueWith((task) => 
            { 
                if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"Successfully connected to {_target}");
                }
            });

            try
            {
                // we need a new client for this service
                var client = new SummarizeService.SummarizeServiceClient(channel);
                
                // this is the request that the service wants
                var request = new SumRequest { Lhs = 3, Rhs = 10 };
                
                // this is the actual request, and it returns a response
                var response = client.Summarize(request);

                Console.WriteLine($"According to the server, {request.Lhs} + {request.Rhs} = {response.Sum}");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"Something went wrong when requesting remote procedure: {e.Message}");
            }
            finally
            {
                // shut down connection when finished
                channel.ShutdownAsync().Wait();
                Console.ReadKey();
            }
        }
    }
}
