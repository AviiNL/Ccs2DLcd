using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
  public class Vector2
  {
    public double X;
    public double Y;

    public Vector2(float x, float y)
    {
      this.X = x;
      this.Y = y;
    }

    public Vector2()
    {
      this.X = 0;
      this.Y = 0;
    }

    public System.Drawing.Point toPoint()
    {
      return new System.Drawing.Point((int)Math.Round(X), (int)Math.Round(Y));
    }
  }
}
