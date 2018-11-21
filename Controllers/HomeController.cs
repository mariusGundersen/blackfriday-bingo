using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blackfriday_bingo.Models;

namespace blackfriday_bingo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var logos = Enumerable.Range(1, 18)
                .Select(i => $"logo-{i}.png")
                .Shuffle()
                .Take(9);
            return View(logos);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
