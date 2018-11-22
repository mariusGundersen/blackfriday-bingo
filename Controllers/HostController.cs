using System.Collections.Generic;
using blackfriday_bingo.Pingdom;
using Microsoft.AspNetCore.Mvc;

namespace blackfriday_bingo.Controllers
{
    public class HostController : Controller
    {
        public IEnumerable<HostStatus> GetAllHostStatuses()
        {
            return HostService.GetAllHostStatuses();
        }

        public IEnumerable<HostReports> GetHostReports([FromQuery(Name = "hostName")] string hostName)
        {
            return HostService.GetHostReports(hostName);
        }
    }
}
