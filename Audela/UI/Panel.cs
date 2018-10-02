using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.UI
{
    class Panel
    {
        private static int systemPanelX;
        public int SystemPanelX
        {
            get
            {
                return systemPanelX;
            }
        }

        public static void DrawPanel(int SystemInfoPanelX)
        {
            SystemInfoPanel(SystemInfoPanelX);
        }

        static void SystemInfoPanel(int x)
        {
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(x, i);      
                Console.Write(" ");
            }
            Console.ResetColor();
        }
    }
}
