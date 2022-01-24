using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PatientsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemographicsController : ControllerBase
    {
        [HttpGet]
        // [Authorize("dataEventRecordsUser")]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(100); // simulate latency
            var items = new List<string>()
            {
               "TestUser", "Bangalore"
            };

            return Ok(items);
        }

        //  [Authorize("dataEventRecordsAdmin")]
        [HttpPost]
        [Route("postdata")]
        public async Task<IActionResult> Post(string input)
        {
            await Task.Delay(100); // simulate latency

            return Ok($"called by demographics admin, data :{input}");
        }
    }
}
