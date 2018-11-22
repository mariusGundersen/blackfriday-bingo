using System.Collections.Generic;
using BlackFridayBingo.Pingdom;
using Microsoft.AspNetCore.Mvc;

namespace BlackFridayBingo.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ApiController : Controller
    {
        [Route("victims")]
        public IEnumerable<Victim> Victims()
        {
            return Config.Victims;
        }
    }
}
