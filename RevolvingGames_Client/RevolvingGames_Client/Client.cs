using Grpc.Core;
using Grpc.Net.Client;
using RevolvingGamesClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolvingGames_Client
{
    public class Client
    {
        ConcurrentDictionary<long, long> rrt;
        AsyncDuplexStreamingCall<PrimeNumber, PrimeNumberResp> call;
        GrpcChannel channel;
        public Client() 
        {

            channel = GrpcChannel.ForAddress("http://localhost:5173");
            var client = new RevolvingService.RevolvingServiceClient(channel);
            rrt = new();

            call = client.CheckPrimeNmber();
            
        }
        private async Task SendRequests()
        {
            await Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine("Send Init");
                    for (long i = 1; i <= 10000; i++)
                    {
                        PrimeNumber req = new()
                        {
                            Id = i,
                            Number = new Random().Next(1000),
                            Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                        };
                        rrt.TryAdd(i, req.Timestamp);
                        await call.RequestStream.WriteAsync(req);
                    }

                    // Complete sending messages
                    await call.RequestStream.CompleteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending requests: " + ex.StackTrace + "\n" + ex.Message);
                }
            });
        }

        private async Task Receive()
        {
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("Received from server: " + response.ToString() + " within " + (DateTimeOffset.Now.ToUnixTimeMilliseconds() - rrt.GetValueOrDefault(response.Id)) + " ms");
                rrt.Remove(response.Id, out _);
            }
        }
        public async Task ClientExeAsync()
        {
            Console.WriteLine("Client running");

            var sendRequestsTask = SendRequests();

            try
            {
                Console.WriteLine("Receive Init");
                await Receive();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error receiving responses: " + ex.Message);
            }

            await sendRequestsTask;
            channel.Dispose();

        }
    }
}
