using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iosha.RaspberryPi3WebControl.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedController : ControllerBase
    {
        private readonly IWs2812bAdapter _ws2812bAdapter;

        public LedController(IWs2812bAdapter ws2812BAdapter)
        {
            _ws2812bAdapter = ws2812BAdapter;
        }

        [HttpGet]
        public async Task<IActionResult> ChangeColor()
        {
            var random = new Random();
            var color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

            _ws2812bAdapter.SetAll(color);
            _ws2812bAdapter.Update();
            return Ok();
        }
    }
}
