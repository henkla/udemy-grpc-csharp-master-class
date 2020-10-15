using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        private const string target = "localhost:50051";
        static void Main(string[] args)
        {
            var channel = new Channel(target, ChannelCredentials.Insecure);
            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            //var client = new DummyService.DummyServiceClient(channel);
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting { FirstName = "Henrik", LastName = "Larsson" };
            var response = client.Greet(new GreetingRequest { Greeting = greeting });

            Console.WriteLine($"Response: {response.Result}");

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
