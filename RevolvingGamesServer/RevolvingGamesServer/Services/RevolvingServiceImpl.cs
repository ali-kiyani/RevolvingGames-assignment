using Grpc.Core;
using RevolvingGamesServer;
using System.Collections;
using System.Collections.Concurrent;

namespace RevolvingGamesServer.Services
{
    public class RevolvingServiceImpl : RevolvingService.RevolvingServiceBase
    {
        ConcurrentDictionary<long, int> trackMap = new();
        bool isPrinting = false;
        int totalRequests = 0;
        public RevolvingServiceImpl()
        {
            Console.Clear();
            _ = PrintTopPrimeNumbers();
        }

        public override async Task CheckPrimeNmber(IAsyncStreamReader<PrimeNumber> requestStream, IServerStreamWriter<PrimeNumberResp> responseStream, ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                Interlocked.Increment(ref totalRequests);
                // Process the incoming request and generate a response
                var response = new PrimeNumberResp()
                {
                    Id = request.Id,
                    IsPrime = await IsPrime(request.Number),
                    Number = request.Number
                };
                if (response.IsPrime)
                {
                    trackMap.AddOrUpdate(request.Number, 1, (key, oldValue) => oldValue + 1);
                }
                await responseStream.WriteAsync(response);
            }
        }

        public async Task PrintTopPrimeNumbers()
        {
            if (isPrinting) return;
            await Task.Run(async () =>
            {
                while (true)
                {
                    isPrinting = true;
                    if (!trackMap.IsEmpty)
                    {
                        Console.Clear();
                        Console.WriteLine("*** Top 10 Prime Numbers ***\n");
                        var l = trackMap.ToList();
                        l.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));
                        l.Take(10).ToList().ForEach(x => Console.WriteLine("Number: " + x.Key + " requested " + x.Value + " times"));
                        Console.WriteLine("\n*** Total requests received are: " + totalRequests + " ***");
                    }
                    await Task.Delay(1000);
                }
            });
        }

        public async Task<bool> IsPrime(long number)
        {
            var res = Task.Run(() =>
            {
                if (number <= 1)
                {
                    return false;
                }
                if (number <= 3)
                {
                    return true;
                }
                if (number % 2 == 0 || number % 3 == 0)
                {
                    return false;
                }
                int i = 5;
                while (i * i <= number)
                {
                    if (number % i == 0 || number % (i + 2) == 0)
                    {
                        return false;
                    }
                    i += 6;
                }
                return true;
            });
            return await res;
        }
    }
}