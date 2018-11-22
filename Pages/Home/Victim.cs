using System;

namespace BlackFridayBingo
{
    public class Victim
    {

        private static readonly Random Random = new Random();

        public Victim(string id, string url)
        {
            Id = id;
            Url = url;
        }

        public string Id { get; }

        public string Url { get; }

        public bool IsAlive => Random.NextDouble() > 0.5;
    }
}