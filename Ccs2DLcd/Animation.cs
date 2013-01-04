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

    private bool animating = true;
    private int startFrame = -1;

    public Animation(string file, int cols, int rows):
      base(file)
    {
      this.cols = cols;
      this.rows = rows;
      frameWidth = getBitmap().Width / cols;
      frameHeight = getBitmap().Height / rows;

      Size = new System.Drawing.Size(frameWidth, frameHeight);
      Rectangle = new System.Drawing.Rectangle(0, 0, frameWidth, frameHeight);
    }
    /// <summary>
    /// Initialize a bitmap for animation
    /// </summary>
    /// <param name="bitmap">use Engine.content.Load<Bitmap>(string filename)</param>
    /// <param name="cols">Amount of columns in the spritesheet</param>
    /// <param name="rows">Amount of rows in the spritesheet</param>
    public Animation(System.Drawing.Bitmap bitmap, int cols, int rows) :
      base(bitmap)
    {
      this.cols = cols;
      this.rows = rows;
      frameWidth = getBitmap().Width / cols;
      frameHeight = getBitmap().Height / rows;

      Size = new System.Drawing.Size(frameWidth, frameHeight);
      Rectangle = new System.Drawing.Rectangle(0, 0, frameWidth, frameHeight);
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
        frame = startFrame - 1;
      animating = false;
    }

    /// <summary>
    /// Updates the animation
    /// </summary>
    /// <param name="fps">Framerate to update the animation (default: 10 frames per second)</param>
    /// <param name="startFrame">Frame to start the animation (default: 1)</param>
    /// <param name="endFrame">Frame to end the animation (default: -1) [-1 to continue to the last frame]</param>
    public void Update(float fps = 10f, int startFrame = 1, int endFrame = -1)
    {
      if (frame == -1 || this.startFrame != startFrame-1)
      {
        this.startFrame = startFrame - 1;
        frame = startFrame - 1;
      }
      timer += (float)Engine.ElapsedGameTime.TotalSeconds;

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
    }

  }
}
