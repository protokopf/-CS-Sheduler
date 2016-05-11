using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.WindowTypes;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View
{
    class ConsoleView
    {
        private BasicWindow mBasicWindow = null;
        private BasicWindow mActiveWindow = null;
        private Drawer mWindowDrawer;

        public void RecursiveAddingToDrawerList(BasicWindow window)
        {
            mWindowDrawer.AddGoal(window);
            foreach (var child in window.Childs)
                RecursiveAddingToDrawerList(child);
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
            RecursiveAddingToDrawerList(mActiveWindow);
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
            mWindowDrawer = new Drawer();
            DesignConsole();
        }

        public void MainLoop()
        {
            mWindowDrawer.InitialDrawing();
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    mActiveWindow.KeyReact(Console.ReadKey(), mActiveWindow);
                }
                mWindowDrawer.Draw();
            }
        }



    }
}
