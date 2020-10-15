using Grpc.Core;
using Sum;
using System.Threading.Tasks;

namespace SumAPI.Server.Services
{
    public class SummarizeService : Sum.SummarizeService.SummarizeServiceBase
    {
        public override Task<SumResponse> Summarize(SumRequest request, ServerCallContext context)
        {
            return base.Summarize(request, context);
        }
    }
}
