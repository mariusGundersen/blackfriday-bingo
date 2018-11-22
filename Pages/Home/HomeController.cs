using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlackFridayBingo.Models;
using Microsoft.AspNetCore.Http;

namespace BlackFridayBingo.Pages.Home
{
    public class HomeController : Controller
    {
        private static readonly IReadOnlyCollection<Victim> Victims = new List<Victim>
        {
            new Victim("1", "https://www.skousen.no/"),
            new Victim("2", "https://www.elkjop.no/"),
            new Victim("3", "https://www.power.no/"),
            new Victim("4", "https://www.dustin.no/"),
            new Victim("5", "https://www.fjellsport.no/"),
            new Victim("6", "https://www.coolstuff.no/"),
            new Victim("7", "https://www.br.no/"),
            new Victim("8", "https://www.whiteaway.no/"),
            new Victim("9", "https://www.soundgarden.no/"),
            new Victim("10", "https://www.cdon.no/"),
            new Victim("11", "https://www.hifiklubben.no/"),
            new Victim("12", "https://www.netonnet.no/"),
            new Victim("13", "https://www.xxl.no/"),
            new Victim("14", "https://www.stormberg.no/"),
            new Victim("15", "https://www.bildeler.no/"),
            new Victim("16", "https://www.multicom.no/"),
            new Victim("17", "https://no.coolshop.com/"),
            new Victim("18", "https://www.komplett.no/"),
        };

        public IActionResult Index()
        {
            var model = GetOrCreateBoard();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<string> GetOrCreateBoard(){
            if(Request.Cookies.TryGetValue("bingo-board", out var board)){
                return board.Split("-");
            }else{
                var logos = Victims
                    .Shuffle()
                    .Select(i => i.Image)
                    .Take(9)
                    .ToList();

                Response.Cookies.Append("bingo-board", string.Join("-", logos), new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddYears(1),
                    Path = "/"
                });

                return logos;

            }
        }
    }
}
