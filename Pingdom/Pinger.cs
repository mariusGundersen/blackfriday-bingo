using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlackFridayBingo.Pingdom
{
    public class Pinger
    {
        private readonly Uri _uri;
        private const int HttpTimeOut = 10_000;

        private readonly HttpClient _jsonClient;

        public Pinger(Uri uri)
        {
            _uri = uri;
            _jsonClient = new HttpClient
            {
                BaseAddress = uri
            };
        }

        public async Task Ping()
        {
            using (var cancellationTokenSource = new CancellationTokenSource(HttpTimeOut))
            {
                try
                {
                    var result = await _jsonClient.GetAsync(_uri.PathAndQuery, cancellationTokenSource.Token);
                    if (result.IsSuccessStatusCode)
                    {
                        SetAlive(true);
                        return;
                    }
                }
                catch (Exception e)
                {
                }
            }

            SetAlive(false);
        }

        void SetAlive(bool isAlive)
        {
            Config.Victims.First(x => new Uri(x.Url) == _uri).History.Enqueue(isAlive);
        }
    }
}