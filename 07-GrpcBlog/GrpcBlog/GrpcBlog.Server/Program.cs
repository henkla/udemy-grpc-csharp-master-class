using Blog;
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using GrpcBlog.Server.Services;
using System;
using System.IO;

namespace GrpcBlog.Server
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50056;

        static void Main(string[] args)
        {
            Grpc.Core.Server server = null;

            try
            {
                var reflectionServiceImpl = new ReflectionServiceImpl(BlogService.Descriptor, ServerReflection.Descriptor);
                server = new Grpc.Core.Server()
                {
                    Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) },
                    Services = 
                    { 
                        BlogService.BindService(new BlogServiceImpl()),
                        ServerReflection.BindService(reflectionServiceImpl)
                    }
                };

                server.Start();
                Console.WriteLine($"Server is listening on {_host}:{_port}");
            }
            catch (IOException e)
            {
                Console.WriteLine($"Something happened while trying to start the server: {e.Message}");
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
