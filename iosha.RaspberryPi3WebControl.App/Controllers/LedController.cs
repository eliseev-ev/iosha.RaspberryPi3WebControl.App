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
        private readonly IWs2812bManager _ws2812bManger;

        public LedController(IWs2812bManager ws2812bManger)
        {
            _ws2812bManger = ws2812bManger;
        }


        [HttpGet("automode")]
        public async Task<IActionResult> Automode()
        {
            _ws2812bManger.SetAutomode(!_ws2812bManger.IsAutomode);
            return Ok();
        }

        [HttpGet("{red}/{green}/{blue}")]
        public async Task<IActionResult> SetCustomColor(byte red, byte green, byte blue)
        {
            var color = Color.FromArgb(red, green, blue);

            _ws2812bManger.SetAllPixels(color);
            return Ok();
        }

        [HttpGet("random")]
        public async Task<IActionResult> SetCustomColor()
        {
            var random = new Random();
            var color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

            _ws2812bManger.SetAllPixels(color);
            return Ok();
        }
    }
}
