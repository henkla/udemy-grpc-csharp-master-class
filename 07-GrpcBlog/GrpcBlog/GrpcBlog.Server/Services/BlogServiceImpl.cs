using Blog;
using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
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
    }
}
