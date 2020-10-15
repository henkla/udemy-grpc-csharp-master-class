using Grpc.Core;
using System;
using System.IO;

namespace SumAPI.Server
{
    class Program
    {
        private const int _port = 500001;
        private const string _host = "localhost";

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                server = new Grpc.Core.Server
                {
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine($"The server is listening on {_host}:{_port}");
                Console.ReadKey();

            }
            catch (IOException e)
            {
                Console.WriteLine($"The server failed to start: {e.Message}");
                Console.ReadKey();
            }
            finally
            {
                if (server != null)
                    server.ShutdownAsync().Wait();

                Console.WriteLine($"The server has been shut down - press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
