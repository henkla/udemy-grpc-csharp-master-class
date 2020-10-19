using Blog;
using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace GrpcBlog.Server.Services
{
    public class BlogServiceImpl : BlogService.BlogServiceBase
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BsonDocument> _mongoCollection;

        public BlogServiceImpl()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _mongoDatabase = _mongoClient.GetDatabase("myDb");
            _mongoCollection = _mongoDatabase.GetCollection<BsonDocument>("blog");
        }

        public override Task<CreateBlogResponse> CreateBlog(CreateBlogRequest request, ServerCallContext context)
        {
            var blog = request.Blog;
            var doc = new BsonDocument("author_id", blog.AuthorId)
                                  .Add("title", blog.Title)
                                  .Add("content", blog.Content);

            _mongoCollection.InsertOne(doc);
            blog.Id = doc.GetValue("_id").ToString();

            return Task.FromResult(new CreateBlogResponse() { Blog = blog });
        }

        public override async Task<ReadBlogResponse> ReadBlog(ReadBlogRequest request, ServerCallContext context)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(request.Id));
            var result = await _mongoCollection.Find(filter).FirstOrDefaultAsync();

            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"The blog with id {request.Id} was not found in the database"));

            try
            {
                var blog = new Blog.Blog
                {
                    Id = result.GetValue("_id").ToString(),
                    AuthorId = result.GetValue("author_id").ToString(),
                    Title = result.GetValue("title").ToString(),
                    Content = result.GetValue("content").ToString()
                };

                return new ReadBlogResponse { Blog = blog };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while building blog post: {e.Message}");
                throw new RpcException(new Status(StatusCode.Internal, $"An error occured while retrieving the blog post"));
            }
        }
    }
}
