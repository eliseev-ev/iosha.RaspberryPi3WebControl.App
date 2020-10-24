using System.Drawing;

namespace iosha.RaspberryPi3WebControl
{
    public interface IWs2812bManager
    {
        bool IsAutomode { get; }

        void Clear();
        void SetAllPixels(Color color);
        void SetAutomode(bool flag);
        void SetPixel(int position, Color color);

        void SaveSchema();
    }
}