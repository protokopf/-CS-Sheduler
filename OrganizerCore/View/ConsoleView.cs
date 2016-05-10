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

        private void ClearScreen()
        {
            if(mActiveWindow != null)
                mActiveWindow.Clear();
        }
        private void DrawScreen()
        {
            if(mActiveWindow != null)
                mActiveWindow.Draw();
        }

        private void DesignConsole()
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(SizeX, SizeY);
            Console.WindowHeight = SizeY + 1;
            Console.WindowWidth = SizeX + 1;
            Console.Title = "Sheduler";
            mBasicWindow = new PluralWindow()
            {
                BackgroundColor = ConsoleColor.White,
                FontColor = ConsoleColor.Black,
                PositionX = 0,
                PositionY = 0,
                Width = SizeX,
                Height = SizeY,
                IsHidden = false, 
                Parent = null
            };
            BasicWindow b1 = new PluralWindow()
            {
                BackgroundColor = ConsoleColor.White,
                FontColor = ConsoleColor.Black,
                PositionX = 48,
                PositionY = 15,
                Width = 21,
                Height = 15,
                IsHidden = false,
                Parent = null
            };

            BasicWindow b2 = new PluralWindow()
            {
                BackgroundColor = ConsoleColor.White,
                FontColor = ConsoleColor.Black,
                PositionX = 15,
                PositionY = 2,
                Width = 21,
                Height = 15,
                IsHidden = false,
                Parent = null
            };

            mBasicWindow.AddChildWindow(b1);
            mBasicWindow.AddChildWindow(b2);
            mActiveWindow = mBasicWindow;
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
            DrawScreen();
            while(true)
            {
                if(Console.KeyAvailable)
                {
                    ClearScreen();
                    mActiveWindow.KeyReact(Console.ReadKey().Key, mActiveWindow);
                    DrawScreen();
                }
                //ClearScreen();
                //DrawScreen();
            }
        }



    }
}
