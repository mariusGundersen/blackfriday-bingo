using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlackFridayBingo.Pingdom
{
    public class PingdomService
    {
        static readonly TimeSpan WaitBetweenCalls = TimeSpan.FromSeconds(15);

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
                await JsonFileSaver.SaveVictimInfoToFile(Config.Victims);
            }
        }
    }
}
