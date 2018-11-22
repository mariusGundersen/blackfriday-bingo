using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace BlackFridayBingo
{
    public class JsonFileSaver
    {
        static string VictimPath;

        public static async Task SaveVictimInfoToFile(IEnumerable<Victim> victims) => await File.WriteAllTextAsync(VictimPath, JsonConvert.SerializeObject(victims));

        public static IReadOnlyList<Victim> ReadFromFile(IHostingEnvironment env)
        {
            var path = Path.Combine(Path.GetDirectoryName(env.WebRootPath), "db", "victims.json");
            VictimPath = path;
            var savableVictims = File.Exists(path) ? JsonConvert.DeserializeObject<List<SavableVictim>>(File.ReadAllText(path)) : null;
            if (savableVictims == null)
            {
                return null;
            }

            var victims = new List<Victim>();
            foreach (var savableVictim in savableVictims)
            {
                var victim = new Victim(savableVictim.Id, savableVictim.Url);
                foreach (var b in savableVictim.History)
                {
                    victim.History.Enqueue(b);
                }

                victims.Add(victim);
            }

            return victims;
        }

        class SavableVictim
        {
            public string Id { get; set; }

            public string Url { get; set; }

            public IEnumerable<bool> History { get; set; }
        }
    }
}