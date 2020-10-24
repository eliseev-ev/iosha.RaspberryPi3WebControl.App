using Iot.Device.Ws28xx;
using Microsoft.Extensions.Options;
using System.Device.Spi;
using System.Drawing;

namespace iosha.RaspberryPi3WebControl
{
    public class Ws2812bAdapter : IWs2812bAdapter
    {
        private readonly Ws2812b _ledTape;
        private readonly Iot.Device.Graphics.BitmapImage _image;
        private readonly int _count;
        public int PixelCount => _count;

        public Ws2812bAdapter(IOptions<LedOptions> options) 
        {
            _count = options.Value.LedCount;

            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_800_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            var spi = SpiDevice.Create(settings);
            _ledTape = new Ws2812b(spi, _count);

            _image = _ledTape.Image;
        }

        public void SetAllPixels(Color color)
        {
            for (int i = 0; i < _count; i++)
                _image.SetPixel(i, 0, color);
        }

        public void SetPixel(int position, Color color)
        {
            _image.SetPixel(position, 0, color);
        }

        public void Update()
        {
            _ledTape.Update();
        }

        public void Clear()
        {
            SetAllPixels(Color.Black);
            Update();
        }
    }
}
