using Grpc.Core;
using Sqrt;
using SquareRootAPI.Server.Services;
using System;
using System.IO;

namespace SquareRootAPI.Server
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50055;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;
            try
            {
                server = new Grpc.Core.Server()
                {
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },
                    Services = { SqrtService.BindService(new SqrtServiceImpl()) }
                };

                server.Start();
                Console.WriteLine($"Listening on {_host}:{_port}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occured while trying to start the server: {e.Message}");
            }
            finally
            {
                Console.ReadKey();
                if (server != null)
                    server.ShutdownAsync().Wait();
            }
        }
    }
}
