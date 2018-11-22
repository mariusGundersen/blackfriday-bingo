using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BlackFridayBingo.Models;
using Microsoft.AspNetCore.Http;

namespace BlackFridayBingo.Pages.Home
{
    public class HomeController : Controller
    {

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

        private IEnumerable<Victim> GetOrCreateBoard()
        {
            if (Request.Cookies.TryGetValue("bingo-board", out var board))
            {
                return board.Split("-").Select(id => Config.Victims.First(v => v.Id == id));
            }

            var logos = Config.Victims
                .Shuffle()
                .Take(9)
                .ToList();

            Response.Cookies.Append("bingo-board", string.Join("-", logos.Select(i => i.Id)), new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1),
                Path = "/"
            });

            return logos;
        }
    }
}
