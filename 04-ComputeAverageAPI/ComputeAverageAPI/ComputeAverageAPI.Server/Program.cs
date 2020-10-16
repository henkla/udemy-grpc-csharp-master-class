using Compute;
using ComputeAverageAPI.Server.Services;
using Grpc.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ComputeAverageAPI.Server
{
    class Program
    {
        private const int _port = 50053;
        private const string _host = "localhost";

        static async Task Main(string[] args)
        {
            // we need the actual server
            Grpc.Core.Server server = null;
            try
            {
                server = new Grpc.Core.Server()
                {
                    // configure the server
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },
                    // register the services it should offer in its API
                    Services = { ComputeAverageService.BindService(new ComputeAverageServiceImpl()) }
                };

                // start the server
                server.Start();
                Console.WriteLine($"The server is listening on {_host}:{_port}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occured while starting the server: {e.Message}");
            }
            finally
            {
                // shut the server down
                Console.ReadKey();
                if (server != null)
                    await server.ShutdownAsync();
            }
        }
    }
}
