using Grpc.Core;
using Sum;
using SumAPI.Server.Services;
using System;
using System.IO;
using SummarizeService = SumAPI.Server.Services.SummarizeService;

namespace SumAPI.Server
{
    class Program
    {
        private const int _port = 50051;
        private const string _host = "localhost";

        static void Main(string[] args)
        {
            // we need an actual server object
            Grpc.Core.Server server = null;

            try
            {
                server = new Grpc.Core.Server
                {
                    // the server needs to be configurated
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },

                    // and handed the services to actually provide
                    Services = { Sum.SummarizeService.BindService(new SummarizeService()) }
                };

                // start the server
                server.Start();
                Console.WriteLine($"The server is listening on {_host}:{_port}");
                Console.ReadKey();

            }
            catch (IOException e)
            {
                // unable to start server
                Console.WriteLine($"The server failed to start: {e.Message}");
                Console.ReadKey();
            }
            finally
            {
                // shut down server when finished
                if (server != null)
                    server.ShutdownAsync().Wait();

                Console.WriteLine($"The server has been shut down - press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
