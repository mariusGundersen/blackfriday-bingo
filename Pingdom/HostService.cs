using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace blackfriday_bingo.Pingdom
{
    public class HostService
    {
        private static readonly ConcurrentDictionary<string, HostStatus> HostStatuses = new ConcurrentDictionary<string, HostStatus>();
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, HostReports>> HostReports = new ConcurrentDictionary<string, ConcurrentDictionary<string, HostReports>>();

        public static void AddReport(PingReport report)
        {
            var host = report.Uri.Host;
            var path = report.Uri.PathAndQuery;

            if (!HostReports.ContainsKey(host))
            {
                HostReports.TryAdd(host, new ConcurrentDictionary<string, HostReports>());
            }

            var hostReports = HostReports[host];
            if (!hostReports.ContainsKey(path))
            {
                hostReports.TryAdd(path, new HostReports{Path = path, PingReport = report});
            }
            else
            {
                hostReports[path].PingReport = report;    
            }

            if (!HostStatuses.ContainsKey(host))
            {
                HostStatuses[host] = new HostStatus {Host = host, Status = GetStatus(hostReports.Values) };
            }
            else
            {
                HostStatuses[host].Status = GetStatus(hostReports.Values);
            }
        }

        public static IEnumerable<HostStatus> GetAllHostStatuses()
        {
            return HostStatuses.Values;
        }

        public static IEnumerable<HostReports> GetHostReports(string hostName)
        {
            HostReports.TryGetValue(hostName, out var result);
            return result?.Values;
        }

        private static Status GetStatus(ICollection<HostReports> reports)
        {
            return reports.Any(x => !x.PingReport.Success)
                ? reports.Any(x => x.PingReport.Success) ? Status.Yellow : Status.Red
                : Status.Green;
        }
    }

    public class HostStatus
    {
        public string Host { get; set; }

        public Status Status { get; set; }
    }

    public class HostReports
    {
        public string Path { get; set; }

        public PingReport PingReport { get; set; }
    }

    public enum Status
    {
        Green,
        Yellow,
        Red
    }
}
