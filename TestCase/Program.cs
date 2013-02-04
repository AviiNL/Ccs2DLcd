using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Ccs2DLcd;

namespace TestCase
{
    class Program
    {
        static Engine engine;
        static Animation sphereLeft;
        static Animation sphereRight;

        static Animation player;

        static Animation trimme;

        static Sprite oneup;

        static Audio tiksound;
        static Audio bgmusic;

        static List<Sprite> sprites = new List<Sprite>();

        static void Main(string[] args)
        {
            engine = new Engine();
            engine.Update += new Engine.UpdateEventHandler(engine_Update);
            engine.SetName("RPG TestCase");
            
            sphereLeft = new Animation(engine.content.Load<Bitmap>("sphere.bmp"), 8, 4);
            sphereLeft.SetTransparent();
            sphereLeft.Position.X = 64;

            sphereRight = new Animation(engine.content.Load<Bitmap>("sphere.bmp"), 8, 4);
            sphereRight.SetTransparent();

            player = new Animation(engine.content.Load<Bitmap>("breeze.png"), 4, 4, true);
            player.SetTransparent();
            player.Position.X = (engine.screen.Size.Width / 2) - (player.Size.Width / 2);
            player.Position.Y = (engine.screen.Size.Height / 2) - (player.Size.Height / 2);

            oneup = new Sprite(engine.content.Load<Bitmap>("1up.png"));
            oneup.Position.X = 128;
            oneup.SetTransparent();
            
            trimme = new Animation(engine.content.Load<Bitmap>("trimme.png"), 1, 1, true);
            trimme.Position.X = 320 - 100;
            trimme.Position.Y = 240 - 100;

            sprites.Add(sphereLeft);
            sprites.Add(sphereRight);
            sprites.Add(player);
            sprites.Add(oneup);
            sprites.Add(trimme);

            bgmusic = engine.content.Load<Audio>("Theme4.ogg");
            bgmusic.Repeat = true;
            bgmusic.Volume = 25;
            bgmusic.Play();

            tiksound = engine.content.Load<Audio>("tik.wav");
            tiksound.Volume = 75;
            //tiksound.Play();
            engine.Start();
        }

        static float directionX = ((float)new Random().Next(1, 100)) / 100;
        static float directionY = ((float)new Random().Next(1, 100)) / 100;

        static int pStart = 1;
        static int pEnd = 4;

        static Buttons oldButtons;

        static void engine_Update(object sender, Buttons buttons)
        {
            // Game logic
            

            oldButtons = buttons;
            player.Update(4, pStart, pEnd);
            trimme.Update(1);
            sphereLeft.Update(28, 32, 1);
            sphereRight.Update(28);

            oneup.Position.X += directionX * (float)Engine.ElapsedGameTime;
            oneup.Position.Y += directionY * (float)Engine.ElapsedGameTime;

            if (oneup.Position.X + oneup.Size.Width >= 320)
            {
                oneup.Position.X = 320 - oneup.Size.Width;
                directionX = -((float)new Random().Next(1, 100)) / 100;
                tiksound.Play();

                pStart = 9;
                pEnd = 12;
            }
            if (oneup.Position.X <= 0)
            {
                oneup.Position.X = 0;
                directionX = ((float)new Random().Next(1, 100)) / 100;
                tiksound.Play();

                pStart = 5;
                pEnd = 8;
            }

            if (oneup.Position.Y + oneup.Size.Height >= 240)
            {
                oneup.Position.Y = 240 - oneup.Size.Height;
                directionY = -((float)new Random().Next(1, 100)) / 100;
                tiksound.Play();

                pStart = 1;
                pEnd = 4;
            }

            if (oneup.Position.Y <= 0)
            {
                oneup.Position.Y = 0;
                directionY = ((float)new Random().Next(1, 100)) / 100;
                tiksound.Play();

                pStart = 13;
                pEnd = 16;
            }

            if (buttons == Buttons.Up)
                player.Stop();
            if (buttons == Buttons.Down)
                player.Start();

            if (buttons == Buttons.Menu)
            {
                sphereLeft.Stop(true);
                sphereRight.Stop(true);
            }

            // Game Drawing
            engine.screen.Clear();
            sprites.ForEach(sprite => engine.screen.Draw(sprite));
            engine.screen.DrawText("FPS: " + engine.FPS.ToString(), 230, 210);
            engine.screen.DrawText("Buttons: " + buttons.ToString());
        }

    }
}
