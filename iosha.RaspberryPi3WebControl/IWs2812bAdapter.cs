﻿using System.Drawing;

namespace iosha.RaspberryPi3WebControl
{
    public interface IWs2812bAdapter
    {
        int PixelCount { get; }

        void Clear();
        void SetAllPixels(Color color);
        void SetPixel(int position, Color color);
        void Update();
    }
}