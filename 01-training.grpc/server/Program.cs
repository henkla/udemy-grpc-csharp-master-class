using Greet;
using Grpc.Core;
using server.services;
using System;
using System.IO;

namespace server
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50051;

        static void Main(string[] args)
        {
            Server server = null;
            
            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()) },
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine($"The server is listening on {_host}:{_port}");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine($"The server failed to start: {e.Message}");
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
