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

    public Sprite(Bitmap bitmap) // TODO: Optimize, do not load same file twice, put them in a resource buffer and re-use when required.
    {
      this.bitmap = new Bitmap(bitmap);
      
      Location = new Vector2(0, 0);
      Size = new System.Drawing.Size(bitmap.Width, bitmap.Height);
      Rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
    }

    public Sprite(string file) // TODO: Optimize, do not load same file twice, put them in a resource buffer and re-use when required.
    {
      if (System.IO.File.Exists(file))
      {
        Log.Write("Loading sprite: [" + System.IO.Path.GetFileName(file) + "]");
        this.bitmap = new Bitmap(file);
        Log.Done();
      }
      else
        throw new System.IO.FileNotFoundException("File not found: " + file);

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
