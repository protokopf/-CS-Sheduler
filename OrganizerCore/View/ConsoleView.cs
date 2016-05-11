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

        private void RecursiveAddingToDrawerList(BasicWindow window)
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
                PositionX = 0,
                PositionY = 0,
                Width = SizeX,
                Height = SizeY,
            };
            BasicWindow sideBlock = new PluralWindow()
            {
                PositionX = SizeX - 25,
                PositionY = 11,
                Width = 25,
                Height = 29,
            };

            BasicWindow buttonAdd = new ButtonWindow("ADD")
            {
                PositionX = SizeX - 18,
                PositionY = 14,
                Width = 12,
                Height = 3
            };
            BasicWindow buttonRemove = new ButtonWindow("REM")
            {
                PositionX = SizeX - 18,
                PositionY = 18,
                Width = 12,
                Height = 3
            };
            sideBlock.AddChildWindow(buttonAdd);
            sideBlock.AddChildWindow(buttonRemove);
            BasicWindow timeBlock = new PluralWindow()
            {
                PositionX = SizeX - 25,
                PositionY = 0,
                Width = 25,
                Height = 11
            };
            BasicWindow runStringBlock = new PluralWindow()
            {
                PositionX = 0,
                PositionY = SizeY - 10,
                Width = SizeX,
                Height = 10,
            };

            mBasicWindow.AddChildWindow(sideBlock);
            mBasicWindow.AddChildWindow(timeBlock);
            mBasicWindow.AddChildWindow(runStringBlock);

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
                    mActiveWindow.KeyReact(Console.ReadKey(true),ref mActiveWindow);
                }
                mWindowDrawer.Draw();
            }
        }

    }
}
