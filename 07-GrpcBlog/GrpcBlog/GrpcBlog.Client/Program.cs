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
        private static readonly string _blogId = "5f8d57e29b63c640b21490b2";

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

            //await CreateBlog(client);
            //await ReadBlog(client);
            //await UpdateBlog(client, "Kjell Jennysson");
            //await DeleteBlog(client);
            await ListBlog(client);

            Console.ReadKey();
            channel.ShutdownAsync().Wait();
        }

        private static async Task ListBlog(BlogService.BlogServiceClient client)
        {
            var response = client.ListBlog(new ListBlogRequest { });

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine($"\n{response.ResponseStream.Current.Blog}\n");
            }
        }

        private static async Task DeleteBlog(BlogService.BlogServiceClient client)
        {
            var blogBefore = await client.ReadBlogAsync(new ReadBlogRequest { Id = _blogId });

            Console.WriteLine($"\nBefore:\n{blogBefore.Blog}");

            var savedBlogPost = await client.DeleteBlogAsync(new DeleteBlogRequest { Id = _blogId });

            Console.WriteLine($"\nSaved:\n{savedBlogPost.Blog}");

            try
            {
                var maybeBlog = await client.ReadBlogAsync(new ReadBlogRequest { Id = _blogId });
                Console.WriteLine($"\nSaved:\n{maybeBlog.Blog}");
            }
            catch (RpcException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static async Task UpdateBlog(BlogService.BlogServiceClient client, string newAuthortitle)
        {
            var blogResponse = await client.ReadBlogAsync(new ReadBlogRequest { Id = _blogId });
            var blog = blogResponse.Blog;

            Console.WriteLine($"\n{blog}");

            blog.AuthorId = newAuthortitle;

            var updateResponse = await client.UpdateBlogAsync(new UpdateBlogRequest { Blog = blog });
            Console.WriteLine($"\n{updateResponse.Blog.ToString()}");
        }

        private static async Task ReadBlog(BlogService.BlogServiceClient client)
        {
            var blog = await client.ReadBlogAsync(new ReadBlogRequest { Id = _blogId });
            Console.WriteLine($"Result for id = {_blogId}:");
            Console.WriteLine(blog.Blog.ToString());
        }

        private static async Task CreateBlog(BlogService.BlogServiceClient client)
        {
            // create a blog post
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
        }
    }
}
