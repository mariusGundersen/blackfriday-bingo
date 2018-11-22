using System.Collections.Generic;
using BlackFridayBingo.Pingdom;
using Microsoft.AspNetCore.Mvc;

namespace BlackFridayBingo.Controllers
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

        [Route("victims")]
        public IEnumerable<Victim> Victims()
        {
            return Config.Victims;
        }
    }
}
