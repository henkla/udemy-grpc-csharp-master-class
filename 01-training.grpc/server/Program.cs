using Greet;
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
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
            // generate and configurate the ssl server credentials
            //var serverCredentials = GenerateServerCredentials();

            // creating a reflection service to register in the server
            // this will allow for users to discover the api endpoints without
            // having access to the .proto files in the first place
            var reflectionServiceImpl = new ReflectionServiceImpl(GreetingService.Descriptor, ServerReflection.Descriptor);

            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()),
                                 ServerReflection.BindService(reflectionServiceImpl)},
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

        private static SslServerCredentials GenerateServerCredentials()
        {
            // get all the certificate files
            var serverCert = File.ReadAllText("ssl/server.crt");
            var serverKey = File.ReadAllText("ssl/server.key");
            var caCert = File.ReadAllText("ssl/ca.crt");

            // create the server keypair
            var keypair = new KeyCertificatePair(serverCert, serverKey);

            // create the actual server credentials configuration providing the ssl details above
            var serverCredentials = new SslServerCredentials(new List<KeyCertificatePair> { keypair }, caCert, true);
            return serverCredentials;
        }
    }
}
