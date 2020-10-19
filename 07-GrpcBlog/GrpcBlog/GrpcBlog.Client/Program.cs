using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcBlog.Client
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50056;

        static async Task Main(string[] args)
        {
            var channel = new Channel($"{_host}:{_port}", ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            { 
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"Successfully connected to {_host}:{_port}");
                }
            });

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
