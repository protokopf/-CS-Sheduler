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

            mBasicWindow = new PluralWindow(0,0,SizeX,SizeY);

            BasicWindow sideBlock = new PluralWindow(SizeX - 25,11,25,35);
            sideBlock.AddChildWindow(new ButtonWindow("ADD", SizeX - 18, 14, 12, 3));
            sideBlock.AddChildWindow(new ButtonWindow("REM", SizeX - 18, 18, 12, 3));

            BasicWindow timeBlock = new PluralWindow(SizeX - 25,0,25,11);
            //// сюда добавится окно с часами

            BasicWindow runStringBlock = new PluralWindow(0,SizeY - 5,SizeX,5);
            runStringBlock.AddChildWindow(new RunningStringWindow(0, SizeY - 5, SizeX, 5));

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
