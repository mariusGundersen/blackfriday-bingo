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
            if(Request.Cookies.TryGetValue("bingo-board", out var board)){
                var logos = board.Split("-");
                return View(logos);
            }else{
                var logos = Enumerable.Range(1, 18)
                    .Shuffle()
                    .Select(i => i.ToString())
                    .Take(9)
                    .ToList();

                Response.Cookies.Append("bingo-board", string.Join("-", logos));
                return View(logos);

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
