using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Ccs2DLcd
{
  public class Sprite
  {
    public Size Size { get; protected set; }
    public Vector2 Location;
    public Rectangle Rectangle;// { get; protected set; }
    private Bitmap bitmap;

    public Sprite(Bitmap bitmap)
    {
      this.bitmap = bitmap;
      
      Location = new Vector2(0, 0);
      Size = new System.Drawing.Size(bitmap.Width, bitmap.Height);
      Rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
    }

    public void SetTransparent()
    {
      this.bitmap.MakeTransparent(this.bitmap.GetPixel(0, 0));
    }

    public void SetTransparent(Color color)
    {
      this.bitmap.MakeTransparent(color);
    }

    public Bitmap getBitmap()
    {
      return bitmap;
    }

  }
}
