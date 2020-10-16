using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace server.services
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            var result = $"Hello, {request.Greeting.FirstName} {request.Greeting.LastName}!";

            return Task.FromResult(new GreetingResponse { Result = result });
        }

        public override async Task GreetManyTimes(GreetManyTimesRequest request, IServerStreamWriter<GreetManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server recieved the request:");
            Console.WriteLine(request.ToString());

            var result = $"Hello, {request.Greeting.FirstName} {request.Greeting.LastName}!";
            foreach (var index in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetManyTimesResponse() { Result = result });
                await Task.Delay(500);
            }
        }
    }
}
