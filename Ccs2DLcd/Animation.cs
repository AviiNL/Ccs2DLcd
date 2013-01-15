using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
  public class Animation : Sprite
  {
    private int frameWidth;
    private int frameHeight;
    private int frame = -1;

    private int cols;
    private int rows;
    float timer;

    int index;
    int row;
    int col;

    private float fps;
    private bool animating = true;
    private int startFrame = -1;
    private int endFrame = -1;

    private bool trim = false;



    /// <summary>
    /// Initialize a bitmap for animation
    /// </summary>
    /// <param name="bitmap">use Engine.content.Load<Bitmap>(string filename)</param>
    /// <param name="cols">Amount of columns in the spritesheet</param>
    /// <param name="rows">Amount of rows in the spritesheet</param>
    public Animation(System.Drawing.Bitmap bitmap, int cols, int rows, bool trim = false) :
      base(bitmap)
    {
      this.cols = cols;
      this.rows = rows;
      this.trim = trim;

      frameWidth = bitmap.Width / cols;
      frameHeight = bitmap.Height / rows;

      Size = new System.Drawing.Size(frameWidth, frameHeight);
      Rectangle = new System.Drawing.Rectangle(0, 0, frameWidth, frameHeight);
      Rectangle = new System.Drawing.Rectangle(0, 0, frameWidth, frameHeight);
      Update(1, 1, 1);
    }

    /// <summary>
    /// Stops the animation
    /// </summary>
    public void Start()
    {
      animating = true;
    }

    /// <summary>
    /// Starts the animation
    /// </summary>
    public void Stop(bool reset = false)
    {
      if (reset)
      {
        frame = -1;
        Update(fps, startFrame + 1, endFrame + 1);
      }
      animating = false;
    }
    private System.Drawing.Size oldSize;

    /// <summary>
    /// Updates the animation
    /// </summary>
    /// <param name="fps">Framerate to update the animation (default: 10 frames per second)</param>
    /// <param name="startFrame">Frame to start the animation (default: 1)</param>
    /// <param name="endFrame">Frame to end the animation (default: -1) [-1 to continue to the last frame]</param>
    public void Update(float fps = 10f, int startFrame = 1, int endFrame = -1)
    {
        if (fps < 0) fps = -fps;

      if (frame == -1 || this.startFrame != startFrame-1)
      {
        this.startFrame = startFrame - 1;
        if(endFrame != -1)
          this.endFrame = endFrame - 1;
        this.fps = fps;
        frame = startFrame - 1;
      }
      timer += (float)Engine.ElapsedGameTime / 1000;

      index = frame % (cols * rows);
      row = index / cols;
      col = index % cols;

      Rectangle.X = col * frameWidth;
      Rectangle.Y = row * frameHeight;

      // Update frame number
      if ((timer >= 1.0f / fps))
      {
        timer -= 1.0f / fps;
        if (animating)
        {
          if (endFrame < startFrame && (endFrame != -1))
          {
            frame--;
            if (frame > (startFrame - 1))
              frame = endFrame - 1;
          }
          else
          {
            frame++;
            if (endFrame != -1 && frame > (endFrame - 1))
              frame = startFrame - 1;
          }
        }
      }


      //if (oldSize.Height < Size.Height)
          //Position.Y -= Size.Height - oldSize.Height;
      //if (oldSize.Width < Size.Width)
          //Position.X -= Size.Width - oldSize.Width;

      oldSize = Size;


    }

    public override System.Drawing.Bitmap getBitmap()
    {
        if (trim)
        {

            Size.Width = bitmap.Clone(Rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Trim().Width;
            Size.Height = bitmap.Clone(Rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Trim().Height;


            return bitmap.Clone(Rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Trim();//, Location.ToPoint());
            
        }

        return bitmap.Clone(Rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);//, Location.ToPoint());
    }
  }
}
