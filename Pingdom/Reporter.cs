using System.Collections.Concurrent;
using System.Diagnostics;

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
    }
}