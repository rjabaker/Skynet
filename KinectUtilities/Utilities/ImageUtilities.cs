using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KinectUtilities
{
    public static class ImageUtilities
    {
        public static Bitmap CreateDefaultBitmap(Size size, Color color)
        {
            Bitmap defaultBitamp = new Bitmap(size.Width, size.Height);

            for (int row = 0; row < size.Width; row++)
            {
                for (int column = 0; column < size.Height; column++)
                {
                    defaultBitamp.SetPixel(row, column, color);
                }
            }

            return defaultBitamp;
        }
    }
}
