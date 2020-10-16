using Grpc.Core;
using Prime;
using PrimeNumberDecompositionAPI.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrimeNumberDecompositionAPI.Server
{
    class Program
    {
        private const int _port = 50052;
        private const string _host = "localhost";

        static void Main(string[] args)
        {
            // we need a server
            Grpc.Core.Server server = null;
            try
            {
                server = new Grpc.Core.Server()
                {
                    // configure the server
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },
                    // bind the services to the server
                    Services = { PrimeNumberDecompositionService.BindService(new PrimeNumberDecompositionServiceImpl()) }
                };

                // start up the server
                server.Start();
                Console.WriteLine($"The server is listening on {_host}:{_port}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The failed to start up: {e.Message}");
            }
            finally
            {
                Console.ReadKey();

                // shut down the server
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
