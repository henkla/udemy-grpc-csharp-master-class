using Grpc.Core;
using Sqrt;
using System;
using System.Threading.Tasks;
using static Sqrt.SqrtService;

namespace SquareRootAPI.Server.Services
{
    public class SqrtServiceImpl : SqrtServiceBase
    {
        public override async Task<SqrtResponse> Sqrt(SqrtRequest request, ServerCallContext context)
        {
            var number = request.Number;

            if (number >= 0)
            {
                return new SqrtResponse { SquareRoot = Math.Sqrt(number) };
            }
            else
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "the provided number must be greater than zero"));
            }
        }
    }
}
