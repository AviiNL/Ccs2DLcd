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

            Device.ButtonsDown += new EventHandler<CsLglcd.ButtonsEventArgs>(Device_ButtonsDown);
            Device.ButtonsUp += new EventHandler<CsLglcd.ButtonsEventArgs>(Device_ButtonsUp);

        }

        private void Device_ButtonsUp(object sender, CsLglcd.ButtonsEventArgs e)
        {
            buttons -= (Buttons)e.Buttons;
        }


        private void Device_ButtonsDown(object sender, CsLglcd.ButtonsEventArgs e)
        {
            buttons += (int)e.Buttons;

        }
    }
}
