using System.Collections.Generic;
using BlackFridayBingo.Pingdom;
using Microsoft.AspNetCore.Mvc;

namespace blackfriday_bingo.Controllers
{
    [ApiController]
    [Route("/api")]
    public class HostController : Controller
    {
        public IEnumerable<HostStatus> GetAllHostStatuses()
        {
            return HostService.GetAllHostStatuses();
        }

        public IEnumerable<HostReports> GetHostReports(string hostName)
        {
            return HostService.GetHostReports(hostName);
        }
    }
}
