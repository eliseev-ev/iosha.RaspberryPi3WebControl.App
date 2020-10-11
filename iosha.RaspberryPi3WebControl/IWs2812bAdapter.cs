using System.Drawing;

namespace iosha.RaspberryPi3WebControl
{
    public interface IWs2812bAdapter
    {
        void SetAll(Color color);
        void SetPixel(int position, Color color);
        void Update();
    }
}