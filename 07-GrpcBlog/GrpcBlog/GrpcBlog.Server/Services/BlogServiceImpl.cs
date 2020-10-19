using Blog;
using Grpc.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public override async Task<UpdateBlogResponse> UpdateBlog(UpdateBlogRequest request, ServerCallContext context)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(request.Blog.Id));
            var result = await _mongoCollection.Find(filter).FirstOrDefaultAsync();

            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"The blog with id {request.Blog.Id} was not found in the database"));

            var doc = new BsonDocument("author_id", request.Blog.AuthorId)
                                  .Add("title", request.Blog.Title)
                                  .Add("content", request.Blog.Content);

            await _mongoCollection.ReplaceOneAsync(filter, doc);

            var blog = new Blog.Blog
            {
                Id = result.GetValue("_id").ToString(),
                AuthorId = doc.GetValue("author_id").ToString(),
                Title = doc.GetValue("title").ToString(),
                Content = doc.GetValue("content").ToString()
            };

            return new UpdateBlogResponse { Blog = blog };
        }

        public override async Task<DeleteBlogResponse> DeleteBlog(DeleteBlogRequest request, ServerCallContext context)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Eq("_id", new ObjectId(request.Id));
            var result = await _mongoCollection.Find(filter).FirstOrDefaultAsync();

            if (result == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"The blog with id {request.Id} was not found in the database"));

            var operationStatus = await _mongoCollection.DeleteOneAsync(filter);

            if (operationStatus.DeletedCount == 0)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"The blog with id {request.Id} could not be deleted"));
            }

            return new DeleteBlogResponse
            {
                Blog = new Blog.Blog
                {
                    Id = result.GetValue("_id").ToString(),
                    AuthorId = result.GetValue("author_id").ToString(),
                    Title = result.GetValue("title").ToString(),
                    Content = result.GetValue("content").ToString()
                }
            };
        }

        public override async Task ListBlog(ListBlogRequest request, IServerStreamWriter<ListBlogResponse> responseStream, ServerCallContext context)
        {
            var filter = new FilterDefinitionBuilder<BsonDocument>().Empty;

            var result = await _mongoCollection.FindAsync(filter);

            foreach (var doc in result.ToList())
            {
                await responseStream.WriteAsync(new ListBlogResponse
                {
                    Blog = new Blog.Blog
                    {
                        Id = doc.GetValue("_id").ToString(),
                        AuthorId = doc.GetValue("author_id").ToString(),
                        Title = doc.GetValue("title").ToString(),
                        Content = doc.GetValue("content").ToString()
                    }
                });
            }
        }
    }
}
