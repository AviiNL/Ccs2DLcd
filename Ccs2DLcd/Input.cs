using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
    public class Input
    {
        //public List<Buttons> buttons{get; private set;}// = new List<Buttons>();
        public Buttons buttons { get; private set; }


        public Input(CsLglcd.Device Device)
        {
            //buttons = new List<Buttons>();
            /*keyb = new Keyboard();
            keyb.KeyDown += new Keyboard.KeyDownEventHandler(keyb_KeyDown);
            keyb.KeyUp += new Keyboard.KeyUpEventHandler(keyb_KeyUp);*/
            Device.ButtonsDown += new EventHandler<CsLglcd.ButtonsEventArgs>(Device_ButtonsDown);
            Device.ButtonsUp += new EventHandler<CsLglcd.ButtonsEventArgs>(Device_ButtonsUp);


        }

        void keyb_KeyUp(object sender, Buttons e)
        {

            buttons = e;
            //if (buttons.HasFlag(e))
            //    buttons -= (int)e;
        }

        void keyb_KeyDown(object sender, Buttons e)
        {
            buttons = e;
            //if (!buttons.HasFlag(e))
            //    buttons += (int)e;
        }

        private void Device_ButtonsUp(object sender, CsLglcd.ButtonsEventArgs e)
        {
            if (buttons.HasFlag((Buttons)e.Buttons))
                buttons -= (int)e.Buttons;

        }


        private void Device_ButtonsDown(object sender, CsLglcd.ButtonsEventArgs e)
        {
            if (!buttons.HasFlag((Buttons)e.Buttons))
                buttons += (int)e.Buttons;

        }
    }
}
