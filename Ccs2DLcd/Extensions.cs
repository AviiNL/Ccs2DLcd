// http://stackoverflow.com/questions/4820212/automatically-trim-a-bitmap-to-minimum-size

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Ccs2DLcd
{
    public static class Extensions
    {
        public static Bitmap Trim(this Bitmap source)
        {
            Rectangle srcRect = default(Rectangle);
            BitmapData data = null;
            try
            {
                data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                byte[] buffer = new byte[Math.Abs(data.Stride) * source.Height];
                
                Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

                int xMin = int.MaxValue,
                    xMax = int.MinValue,
                    yMin = int.MaxValue,
                    yMax = int.MinValue;

                bool foundPixel = false;

                byte firstR = buffer[data.Stride + 4 + 2];
                byte firstG = buffer[data.Stride + 4 + 1];
                byte firstB = buffer[data.Stride + 4 + 0];

                // Find xMin
                for (int x = 0; x < data.Width; x++)
                {
                    bool stop = false;
                    for (int y = 0; y < data.Height; y++)
                    {
                        byte r = buffer[y * data.Stride + 4 * x + 2];
                        byte g = buffer[y * data.Stride + 4 * x + 1];
                        byte b = buffer[y * data.Stride + 4 * x + 0];

                        if (r != firstR || g != firstG || b != firstB)
                        {
                            xMin = x;
                            stop = true;
                            foundPixel = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Image is empty...
                if (!foundPixel)
                    return null;

                // Find yMin
                for (int y = 0; y < data.Height; y++)
                {
                    bool stop = false;
                    for (int x = xMin; x < data.Width; x++)
                    {
                        byte r = buffer[y * data.Stride + 4 * x + 2];
                        byte g = buffer[y * data.Stride + 4 * x + 1];
                        byte b = buffer[y * data.Stride + 4 * x + 0];

                        if (r != firstR || g != firstG || b != firstB)
                        {
                            yMin = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find xMax
                for (int x = data.Width - 1; x >= xMin; x--)
                {
                    bool stop = false;
                    for (int y = yMin; y < data.Height; y++)
                    {
                        byte r = buffer[y * data.Stride + 4 * x + 2];
                        byte g = buffer[y * data.Stride + 4 * x + 1];
                        byte b = buffer[y * data.Stride + 4 * x + 0];

                        if (r != firstR || g != firstG || b != firstB)
                        {
                            xMax = x;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find yMax
                for (int y = data.Height - 1; y >= yMin; y--)
                {
                    bool stop = false;
                    for (int x = xMin; x <= xMax; x++)
                    {
                        byte r = buffer[y * data.Stride + 4 * x + 2];
                        byte g = buffer[y * data.Stride + 4 * x + 1];
                        byte b = buffer[y * data.Stride + 4 * x + 0];

                        if (r != firstR || g != firstG || b != firstB)
                        {
                            yMax = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                srcRect = Rectangle.FromLTRB(xMin, yMin, xMax+1, yMax+1);
            }
            finally
            {
                if (data != null)
                    source.UnlockBits(data);
            }

            return source.Clone(srcRect, source.PixelFormat);
        }
    }
}
