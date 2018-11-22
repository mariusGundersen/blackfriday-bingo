namespace BlackFridayBingo
{
  public class Victim
  {

    public Victim(string image, string url)
    {
      Image = image;
      Url = url;
    }

    public string Image { get; }

    public string Url { get; }
  }
}