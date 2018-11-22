using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace blackfriday_bingo.Pingdom
{
    public class PingdomService
    {
        public static void Run(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                var uri = new Uri(url);
                var pinger = new Pinger(uri);
                var thread = new Thread(pinger.Start) 
                {
                    IsBackground = true
                };
                thread.Start();

                Thread.Sleep(150);
            }
        }
    }
}
