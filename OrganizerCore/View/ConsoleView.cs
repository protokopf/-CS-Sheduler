using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.WindowTypes;

namespace OrganizerCore.View
{
    class ConsoleView
    {
        private BasicWindow mBasicWindow = null;
        private BasicWindow mActiveWindow = null;

        private void DesignConsole()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(SizeX, SizeY);
            mBasicWindow = new PluralWindow()
            {
                BackgroundColor = ConsoleColor.White,
                FontColor = ConsoleColor.Black,
                PositionX = 0,
                PositionY = 0,
                Width = SizeX,
                Height = SizeY,
                IsHidden = false
            }
        }

        public interface ConsoleViewCommunicator
        {
            
        }
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public ConsoleView(int x, int y)
        {
            SizeX = x;
            SizeY = y;
            DesignConsole();
        }

        public void MainLoop()
        {
            while(true)
            {
                if(Console.KeyAvailable)
                {
                    mActiveWindow.KeyReact(Console.ReadKey().Key, mActiveWindow);
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
