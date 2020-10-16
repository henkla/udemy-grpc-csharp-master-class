using Calculator;
using FindMaximumAPI.Server.Services;
using Grpc.Core;
using System;
using System.IO;

namespace FindMaximumAPI.Server
{
    class Program
    {
        private const int _port = 50054;
        private const string _host = "localhost";

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;
            try
            {
                // create the actual server
                server = new Grpc.Core.Server()
                {
                    // configure the server
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },
                    // bind services to the server
                    Services = { CalculatorService.BindService(new CalculatorServiceImpl()) }
                };

                // start up the server
                server.Start();

                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occured while starting up the server: {e.Message}");
            }
            finally
            {
                // shut down the server
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
