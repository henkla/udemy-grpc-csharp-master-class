using Dummy;
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

            //await DoUnaryGreet(client, greeting);
            //await DoStreamingServerGreet(client, greeting);
            //await DoDoStreamingClientGreet(client, greeting);
            //await DoBidirectionalGreet(client);
            await DoUnaryGreetWithDeadline(client, greeting);

            // shut down the connection to the gRPC server
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        private static async Task DoUnaryGreet(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
            /*
                                UNARY REQUEST
                         */
            Console.WriteLine($"Unary request: {greeting}");

            // call the service with the request through the client
            var unaryResponse = client.Greet(new GreetingRequest { Greeting = greeting });

            // check the response from the unary service request
            Console.WriteLine($"Response: {unaryResponse.Result}");
            Console.ReadKey();
        }

        private static async Task DoStreamingServerGreet(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
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
        }

        private static async Task DoDoStreamingClientGreet(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
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
        }

        private static async Task DoBidirectionalGreet(GreetingService.GreetingServiceClient client)
        {
            /*
                BI-DIRECTIONAL STREAMING REQEUST
            */

            var random = new Random();
            Console.WriteLine($"Bidirectional streaming request");

            // the object to store the streams in
            var stream = client.GreetEveryone();

            // read from the response stream - this will be done asynchronosly in a new thread
            var responseReaderTask = Task.Run(async () => 
            { 
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"Recieved: {stream.ResponseStream.Current.Result}");
                }
            });

            // create a number of different greetings
            Greeting[] greetings =
            {
                new Greeting { FirstName = "Sherlock", LastName = "Holmes" },
                new Greeting { FirstName = "Elias", LastName = "Larsson" },
                new Greeting { FirstName = "Alvin", LastName = "Larsson" },
                new Greeting { FirstName = "Zarah", LastName = "Eriksson" }
            };

            // actually send the server a request for each of these greetings
            foreach (var greeting in greetings)
            {
                await Task.Delay(random.Next(400,4000));
                Console.WriteLine($"Sending: {greeting}");
                await stream.RequestStream.WriteAsync(new GreetEveryoneRequest { Greeting = greeting });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;
        }

        private static async Task DoUnaryGreetWithDeadline(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
            try
            {
                var response = await client.GreetWithDeadlineAsync(new GreetingRequest { Greeting = greeting }, deadline: DateTime.UtcNow.AddMilliseconds(500));
                Console.WriteLine($"Response: {response.Result}");
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.DeadlineExceeded)
            {
                Console.WriteLine($"Error - {e.StatusCode}: {e.Status.Detail}");
            }
        }
    }
}
