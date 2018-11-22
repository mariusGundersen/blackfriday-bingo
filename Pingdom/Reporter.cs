using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace blackfriday_bingo.Pingdom
{
    public class Reporter
    {
        public static ConcurrentQueue<PingReport> Queue = new ConcurrentQueue<PingReport>();

        public static void Add(PingReport pingReport)
        {
            Queue.Enqueue(pingReport);
#if DEBUG
            Debug.WriteLine(pingReport.ToString());
#endif
        }

        public static void Start()
        {
            var thread = new Thread(new Reporter().Report) 
            {
                IsBackground = true
            };
            thread.Start();
        }

        public void Report()
        {
            try
            {
                while (true)
                {
                    if (Queue.TryDequeue(out var report))
                    {
                        HostService.AddReport(report);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}