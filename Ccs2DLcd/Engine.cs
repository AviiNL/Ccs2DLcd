using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsLglcd;
using System.Drawing;

namespace Ccs2DLcd
{
  public class Engine
  {

    public delegate void UpdateEventHandler(object sender, Buttons buttons);

    /// <summary>
    /// Handles the update event,
    /// All your game logic goes in here
    /// </summary>
    public event UpdateEventHandler Update;

    public Screen screen { get; private set; }
    public Content content { get; private set; }

    private Lglcd lgLcd;
    private Applet applet;
    private Device<QvgaImageUpdater> QvgaDevice;
    private bool Running = false;
    private System.Diagnostics.Stopwatch sw;
    private Input input;
    
    /// <summary>
    /// Gets the current frame rate
    /// </summary>
    public double FPS { get; private set; }

    public static TimeSpan ElapsedGameTime { get; private set; }

    private int MaxFPS = 60;
    public bool LimitFPS = false;
    int ticks = 0;
    int speed = 0;

    /// <summary>
    /// Initializes Engine
    /// </summary>
    public Engine()
    {
      Console.WriteLine(@"_________         _____  _____             _________               ");
      Console.WriteLine(@"\_   ___ \  _____/ ____\/ ____\____   ____ \_   ___ \ __ ________  ");
      Console.WriteLine(@"/    \  \/ /  _ \   __\\   __\/ __ \_/ __ \/    \  \/|  |  \____ \ ");
      Console.WriteLine(@"\     \___(  <_> )  |   |  | \  ___/\  ___/\     \___|  |  /  |_> >");
      Console.WriteLine(@" \______  /\____/|__|   |__|  \___  >\___  >\______  /____/|   __/ ");
      Console.WriteLine(@"        \/                        \/     \/        \/      |__|    ");
      Console.WriteLine(@"  _________ __            .___.__              ");
      Console.WriteLine(@" /   _____//  |_ __ __  __| _/|__| ____  ______ ©");
      Console.WriteLine(@" \_____  \\   __\  |  \/ __ | |  |/  _ \/  ___/");
      Console.WriteLine(@" /        \|  | |  |  / /_/ | |  (  <_> )___ \ ");
      Console.WriteLine(@"/_______  /|__| |____/\____ | |__|\____/____  >");
      Console.WriteLine(@"        \/                 \/               \/ ");
      Console.WriteLine();

      Console.WriteLine("CoffeeCup Studios 2D LCD Game Engine for the Logitech G19 Keyboard");
      Console.WriteLine("------------------------------------------------------------------");
      Console.WriteLine();

      Log.Write("Initializing LCD");
      lgLcd = new Lglcd();
      Log.Done();
      applet = new Applet();
      QvgaDevice = new Device<QvgaImageUpdater>();
      applet.SupportedDevices = SupportedDevices.QVGA;
      Log.Write("Initializing render area");
      screen = new Screen();
      Log.Done();
      Log.Write("Initializing input");
      sw = new System.Diagnostics.Stopwatch();
      input = new Input(QvgaDevice);
      content = new Content();
      Log.Done();
    }

    /// <summary>
    /// Set the name of your game
    /// </summary>
    /// <param name="title">Game Name</param>
    public void SetName(string name)
    {
      applet.Title = name;
    }

    /// <summary>
    /// Initialize and start the engine's main render loop
    /// </summary>
    public void Start()
    {
      Log.Write("Connecting to LCD");
      try
      {
        applet.Connect();
        QvgaDevice.Applet = applet;
        QvgaDevice.DeviceType = Devices.QVGA;
        QvgaDevice.Attach();
        QvgaDevice.ForegroundApplet = true;
        Log.Done();
      }
      catch (Exception)
      {
        Log.Failed();
      }

      Running = true;

      Console.WriteLine("Starting main render loop.");
      while (Running)
      {
        sw.Start();
        if (LimitFPS)
        {
          ticks = Environment.TickCount;
          if (ticks > speed)
          {
            speed = 1000 / (MaxFPS + 10) + ticks;
            if (Update != null)
              Update.Invoke(this, input.buttons);

            QvgaDevice.SpecializedImageUpdater.SetPixels(screen.bitmap);
            QvgaDevice.Update();
            GC.Collect();
            sw.Stop();

            if (sw.Elapsed.TotalMilliseconds > 0) // division by zero protection, probably wont happen anyway
            {
              ElapsedGameTime = sw.Elapsed;
              FPS = Math.Round(1000 / sw.Elapsed.TotalMilliseconds);
            }
            sw.Reset();
          }
        }
        else
        {
          if (Update != null)
            Update.Invoke(this, input.buttons);

          QvgaDevice.SpecializedImageUpdater.SetPixels(screen.bitmap);
          QvgaDevice.Update();
          GC.Collect();
          sw.Stop();

          if (sw.Elapsed.TotalMilliseconds > 0) // division by zero protection
          {
            ElapsedGameTime = sw.Elapsed;
            FPS = Math.Round(1000 / sw.Elapsed.TotalMilliseconds);
          }
          sw.Reset();
        }
      }
    }

    /// <summary>
    /// Dispose of all components used in the engine
    /// </summary>
    public void Dispose()
    {
      if (applet != null)
      {
        applet.Disconnect();
        applet.Dispose();
      }
      if (QvgaDevice != null)
        QvgaDevice.Dispose();
      if (lgLcd != null)
        lgLcd.Dispose();
      if (screen != null)
        screen.Dispose();

    }

  }
}
