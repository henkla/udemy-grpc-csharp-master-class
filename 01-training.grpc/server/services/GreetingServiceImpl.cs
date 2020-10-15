﻿using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
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
    }
}
