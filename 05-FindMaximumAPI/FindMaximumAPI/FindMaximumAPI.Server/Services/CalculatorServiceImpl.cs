using Calculator;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindMaximumAPI.Server.Services
{
    public class CalculatorServiceImpl : CalculatorService.CalculatorServiceBase
    {
        public override async Task FindMaximum(IAsyncStreamReader<FindMaximumRequest> requestStream, IServerStreamWriter<FindMaximumResponse> responseStream, ServerCallContext context)
        {
            var recievedNumbers = new List<int>();

            while (await requestStream.MoveNext())
            {
                Console.WriteLine($"Recieved request: {requestStream.Current.Number}");
                recievedNumbers.Add(requestStream.Current.Number);
                recievedNumbers.Sort();

                if (requestStream.Current.Number >= recievedNumbers.Last())
                {
                    Console.WriteLine($"Sending response: {recievedNumbers.Last()}");
                    await responseStream.WriteAsync(new FindMaximumResponse { Result = recievedNumbers.Last() });
                }
            }
        }
    }
}
