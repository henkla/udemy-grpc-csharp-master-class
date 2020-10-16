using Compute;
using Grpc.Core;
using System.Threading.Tasks;

namespace ComputeAverageAPI.Server.Services
{
    public class ComputeAverageServiceImpl : ComputeAverageService.ComputeAverageServiceBase
    {
        public override async Task<LongComputeResponse> LongCompute(IAsyncStreamReader<LongComputeRequest> requestStream, ServerCallContext context)
        {
            System.Console.WriteLine("The server recieved a request");

            var counter = 0;
            var sum = 0;

            while (await requestStream.MoveNext())
            {
                System.Console.WriteLine($"- {requestStream.Current.Value}");

                counter = counter + 1;
                sum = sum + requestStream.Current.Value;
            }

            var result = ((double)sum / (double)counter);
            return new LongComputeResponse() { Result = result };
        }
    }
}
