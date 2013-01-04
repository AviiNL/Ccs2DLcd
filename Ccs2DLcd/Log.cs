using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
  public static class Log
  {
    public static void Write(string text)
    {
      Console.Write(text + "... ");
    }

    public static void Done()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("done");
      Console.ForegroundColor = ConsoleColor.Gray;
      
    }

    public static void Failed()
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("failed");
      Console.ForegroundColor = ConsoleColor.Gray;

      Console.WriteLine("Press any key to exit.");
      Console.ReadKey();
      Environment.Exit(0);
    }

  }
}
