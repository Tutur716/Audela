using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Audela.UI;

namespace Audela.UI
{
    class Drawer
    {
        public int WindowSizeX;
        public int WindowSizeY;

        public void Draw()
        {
            while (true)
            {
                //Window.SetWindowSize(WindowSizeX, WindowSizeY);
                Panel.DrawPanel(70);
            }
        }
    }
}
