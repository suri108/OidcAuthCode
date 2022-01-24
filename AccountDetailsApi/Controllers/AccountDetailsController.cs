using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WidgetApi.Models;

namespace CliniciansApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailsController : ControllerBase
    {
        [HttpGet]
       // [Authorize("dataEventRecordsUser")]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(100); // simulate latency
            var items = new List<string>()
            {
               "Account1", "Account2"
            };

            return Ok(items);
        }

        [HttpGet("testrogrant")]
        public async Task<IActionResult> TestROGrantType()
        {
            
            return Ok("Test RO grant type endpoint invoked");
        }

        [HttpGet("testauthcode")]
        public async Task<IActionResult> TestAuthCodeGrantType()
        {
            await Task.Delay(100); // simulate latency
            var items = new List<string>()
            {
               "Test1", "Test2"
            };

            return Ok(items);
        }

        //  [Authorize("dataEventRecordsAdmin")]
        [HttpPost]
        //[Route("postdata")]
        public async Task<IActionResult> Post()
        {
            await Task.Delay(100); // simulate latency
            
            return Ok($"called by accounts admin");
        }
    }
}
