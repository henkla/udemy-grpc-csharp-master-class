﻿using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        private const string target = "localhost:50051";
        static async Task Main(string[] args)
        {
            // we need a channel - the actual connection
            var channel = new Channel(target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                // check if connection was successful
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("The client connected successfully");
            });

            // this is the actual greeting service client - it uses the connection above
            var client = new GreetingService.GreetingServiceClient(channel);

            // this is the data object ()wrapped in a request) that will be sent to the server through the client
            var greeting = new Greeting { FirstName = "Henrik", LastName = "Larsson" };


            /*
                    UNARY REQUEST
             */
            Console.WriteLine($"Unary request: {greeting}");
            
            // call the service with the request through the client
            var unaryResponse = client.Greet(new GreetingRequest { Greeting = greeting });
            
            // check the response from the unary service request
            Console.WriteLine($"Response: {unaryResponse.Result}");
            Console.ReadKey();


            /*
                    STREAMING SERVER REQUEST
             */
            Console.WriteLine($"Server streaming request: {greeting}");

            // the streaming server needs a specific request (wrapping the same data object as in the unary case)
            var streamingServerResponse = client.GreetManyTimes(new GreetManyTimesRequest { Greeting = greeting });

            // check the response from the streaming server request
            while (await streamingServerResponse.ResponseStream.MoveNext())
            {
                Console.WriteLine($"Response: {streamingServerResponse.ResponseStream.Current.Result}");
            }
            Console.ReadKey();


            /*
                    STREAMING CLIENT REQUEST
             */
            Console.WriteLine($"Client streaming request: {greeting}");
            var longGreetRequest = new LongGreetRequest() { Greeting = greeting };
            var streamingClientResponseStream = client.LongGreet();
            foreach (var i in Enumerable.Range(1, 10))
            {
                await Task.Delay(1000);
                Console.WriteLine($"- Request: {greeting}");
                await streamingClientResponseStream.RequestStream.WriteAsync(longGreetRequest);
            }

            await streamingClientResponseStream.RequestStream.CompleteAsync();
            var longGreetResponse = await streamingClientResponseStream.ResponseAsync;
            Console.WriteLine($"The server responded with the following:\n{longGreetResponse.Result}");
            Console.ReadKey();


            // shut down the connection to the gRPC server
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
