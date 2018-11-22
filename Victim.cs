using System.Collections.Concurrent;
using System.Linq;

namespace BlackFridayBingo
{
    public class Victim
    {
        public Victim(string id, string url)
        {
            Id = id;
            Url = url;
        }

        public string Id { get; }

        public string Url { get; }

        public FixedSizedQueue<bool> History { get; set; } = new FixedSizedQueue<bool>(60*3);

        public bool IsAlive => History.IsEmpty || History.Last();

        public class FixedSizedQueue<T> : ConcurrentQueue<T>
        {
            private readonly object syncObject = new object();

            public int Size { get; }

            public FixedSizedQueue(int size)
            {
                Size = size;
            }

            public new void Enqueue(T obj)
            {
                base.Enqueue(obj);
                lock (syncObject)
                {
                    while (base.Count > Size)
                    {
                        T outObj;
                        base.TryDequeue(out outObj);
                    }
                }
            }
        }
    }
}