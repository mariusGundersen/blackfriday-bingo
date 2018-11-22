using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackFridayBingo.Pingdom
{
    public class PingdomService
    {
        static readonly TimeSpan WaitBetweenCalls = TimeSpan.FromMinutes(1);

        public static async void Run(IEnumerable<string> urls)
        {
            var pingers = urls.Select(x => new Pinger(new Uri(x))).ToList();
            while (true)
            {
                await Task.WhenAll(pingers.Select(x => x.Ping())
                    .Concat(new[]
                    {
                        Task.Delay(WaitBetweenCalls)
                    }).ToArray());
            }
        }
    }
}
