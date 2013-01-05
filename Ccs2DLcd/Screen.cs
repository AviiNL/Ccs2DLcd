using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ccs2DLcd
{
  public class Screen
  {
    internal Bitmap bitmap { get; private set; }
    public Size Size { get; private set; }
    Graphics g;

    public Screen()
    {
      bitmap = new Bitmap(320, 240);
      Size = new System.Drawing.Size(bitmap.Width, bitmap.Height);
      g = Graphics.FromImage(bitmap);
    }

    public void Clear(Color color)
    {
      g.FillRectangle(new SolidBrush(color), 0, 0, 320, 240);
    }

    public void Clear()
    {
      Clear(Color.DarkSlateBlue);
    }

    public void Draw(Bitmap sprite, float x, float y)
    {
      g.DrawImage(sprite, x, y);
    }

    public void Draw(Sprite sprite)
    {
        g.DrawImage(sprite.getBitmap(), sprite.Location.ToPoint());
    }

    public void DrawText(string text, int x, int y)
    {
      g.DrawString(text, new Font("monospace", 12f), Brushes.White, x, y);
    }

    public void DrawText(string text)
    {
      DrawText(text, 15, 15);
    }

    public void Dispose()
    {
      bitmap.Dispose();
    }
  }
}
