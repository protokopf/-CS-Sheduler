using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View
{
    class ConsoleView
    {
        private List<MyWindow> mWindows;
        private MyWindow mActiveWindow;

        public interface ConsoleViewCommunicator
        {
            
        }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public ConsoleView(int x, int y)
        {
            SizeX = x;
            SizeY = y;
            Console.SetWindowSize(x, y);
        }

        public void MainLoop()
        {
            while(true)
            {
                if(Console.KeyAvailable)
                {
                    // обработать нажатия клавиш
                }
                ClearScreen();
                UpdateListeners();
                DrawScreen();
            }
        }

        public void UpdateListeners()
        {

        }
        public void ClearScreen() 
        {
        }
        public void DrawScreen() 
        { 
        }

    }
}
