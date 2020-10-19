using Blog;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace GrpcBlog.Client
{
    class Program
    {
        private const string _host = "localhost";
        private const int _port = 50056;

        static async Task Main(string[] args)
        {
            var channel = new Channel($"{_host}:{_port}", ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            { 
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine($"Successfully connected to {_host}:{_port}");
                }
            });

            var client = new BlogService.BlogServiceClient(channel);
            var response = await client.CreateBlogAsync(new CreateBlogRequest 
            { 
                Blog = new Blog.Blog
                {
                    AuthorId = "Heinrich Larsson",
                    Title = "How to not screw up",
                    Content = "Screwing up is bad. You should avoid it by any means possible. Stay safe."
                }
            });

            Console.WriteLine($"The Id of the new blog is {response.Blog.Id}");

            var blog = await client.ReadBlogAsync(new ReadBlogRequest { Id = response.Blog.Id });
            Console.WriteLine($"Result for id = {response.Blog.Id}:");
            Console.WriteLine(blog.Blog.ToString());

            Console.ReadKey();
            channel.ShutdownAsync().Wait();
        }
    }
}
