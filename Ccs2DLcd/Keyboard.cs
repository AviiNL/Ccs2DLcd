using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace Ccs2DLcd
{
    public class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        public delegate void KeyUpEventHandler(object sender, Buttons buttons);
        public delegate void KeyDownEventHandler(object sender, Buttons buttons);

        public event KeyUpEventHandler KeyUp;
        public event KeyDownEventHandler KeyDown;
        public Keyboard()
        {
            new Thread(new ThreadStart(Update)).Start();
        }

        Buttons buttons;

        private void Update()
        {
            List<int> keysDown = new List<int>();
            while (true)
            {
                //sleeping for while, this will reduce load on cpu
                Thread.Sleep(10);
                for (Int32 i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767 || keyState == -32768)
                    {
                        if (!keysDown.Contains(i))
                        {
                            keysDown.Add(i);
                            // 37 38 39 40
                            // left up right down
                            if (i == 37)
                                buttons += (int)Buttons.Left;
                            if (i == 38)
                                buttons += (int)Buttons.Up;
                            if (i == 39)
                                buttons += (int)Buttons.Right;
                            if (i == 40)
                                buttons += (int)Buttons.Down;

                            KeyDown(this, buttons);
                            // event KeyDown
                        }
                        
                        break;
                    }
                    if (keyState == 0 && keysDown.Contains(i))
                    {

                        keysDown.Remove(i);
                        if (i == 37)
                            buttons -= (int)Buttons.Left;
                        if (i == 38)
                            buttons -= (int)Buttons.Up;
                        if (i == 39)
                            buttons -= (int)Buttons.Right;
                        if (i == 40)
                            buttons -= (int)Buttons.Down;

                        KeyUp(this, buttons);
                    }
                }
            }
        }
        

    }
}
