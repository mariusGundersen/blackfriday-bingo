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

        public bool IsAlive { get; set; } = true;
    }
}