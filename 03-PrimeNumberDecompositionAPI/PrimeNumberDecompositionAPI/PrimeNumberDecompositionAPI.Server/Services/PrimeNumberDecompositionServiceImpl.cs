using Grpc.Core;
using Prime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumberDecompositionAPI.Server.Services
{
    public class PrimeNumberDecompositionServiceImpl : PrimeNumberDecompositionService.PrimeNumberDecompositionServiceBase
    {
        public override async Task Decompose(PrimeNumberDecompositionRequest request, IServerStreamWriter<PrimeNumberDecompositionResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"The server recieved the request: {request.Number}");

            var number = request.Number;
            var primeFactor = 2;
            while (number > 1)
            {
                if (number % primeFactor == 0)
                {
                    await Task.Delay(2000);
                    await responseStream.WriteAsync(new PrimeNumberDecompositionResponse() { PrimeNumber = primeFactor });
                    number /= primeFactor;
                }
                else
                {
                    primeFactor++;
                }
            }
        }
    }
}
