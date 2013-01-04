using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
  [Flags]
  public enum Buttons
  {
    None = 0,
    Button0 = 1,
    Button1 = 2,
    Button2 = 4,
    Button3 = 8,
    [Obsolete]
    Button4 = 16,
    [Obsolete]
    Button5 = 32,
    [Obsolete]
    Button6 = 64,
    [Obsolete]
    Button7 = 128,
    Left = 256,
    Right = 512,
    Ok = 1024,
    Back = 2048,
    Up = 4096,
    Down = 8192,
    Menu = 16384,
  }
}
