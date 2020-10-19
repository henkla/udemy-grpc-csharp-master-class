using Greet;
using Grpc.Core;
using server.services;
using System;
using System.Collections.Generic;
using System.IO;

namespace server
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50051;

        static void Main(string[] args)
        {
            // get all the certificate files
            var serverCert = File.ReadAllText("ssl/server.crt");
            var serverKey = File.ReadAllText("ssl/server.key");
            var caCert = File.ReadAllText("ssl/ca.crt");

            // create the server keypair
            var keypair = new KeyCertificatePair(serverCert, serverKey);

            // create the actual server credentials configuration providing the ssl details above
            var serverCredentials = new SslServerCredentials(new List<KeyCertificatePair> { keypair }, caCert, true);

            Server server = null;
            
            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()) },
                    Ports = { new ServerPort(_host, _port, serverCredentials) }
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
