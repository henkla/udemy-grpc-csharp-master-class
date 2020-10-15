using Grpc.Core;
using Sum;
using System.Threading.Tasks;

namespace SumAPI.Server.Services
{
    public class SummarizeService : Sum.SummarizeService.SummarizeServiceBase
    {
        private readonly CalculatorService _calculator;
        
        public SummarizeService()
        {
            _calculator = new CalculatorService();
        }

        public override Task<SumResponse> Summarize(SumRequest request, ServerCallContext context)
        {
            var sum = _calculator.Sum(request.Lhs, request.Rhs);
            return Task.FromResult(new SumResponse { Sum = sum });
        }
    }
}
