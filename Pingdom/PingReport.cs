using System;
using System.Net;

namespace blackfriday_bingo.Pingdom
{
    public class PingReport
    {
        public Uri Uri { get; set; }

        public bool Success { get; set;}

        public long Latency { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string Error { get; set; }

        public override string ToString()
        {
            return $"Url: {Uri.ToString()}, {Success}, {StatusCode}, Latency: {Latency} Error: {Error}";
        }

        public static PingReport CreateSuccess(Uri uri, long latency)
        {
            return new PingReport
            {
                Success = true,
                Uri = uri,
                Latency = latency,
                StatusCode = HttpStatusCode.OK
            };
        }

        public static PingReport CreateFailure(Uri uri, long latency, HttpStatusCode statusCode, string error)
        {
            return new PingReport
            {
                Uri = uri,
                Latency = latency,
                StatusCode = statusCode,
                Error = error
            };
        }
    }
}