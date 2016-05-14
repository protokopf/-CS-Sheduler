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
        private IWindowDesigner mWindowDesigner;

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

            mBasicWindow = new PluralWindow("Main",0,0,SizeX,SizeY);

            BasicWindow sideBlock = new PluralWindow("Side",SizeX - 25,11,25,34);
            sideBlock.AddChildWindow(new ButtonWindow("ADD", SizeX - 18, 14, 12, 3));
            sideBlock.AddChildWindow(new ButtonWindow("REM", SizeX - 18, 18, 12, 3));

            BasicWindow timeBlock = new CurrentTimeWindow("Time", SizeX - 25, 0, 25, 3);

            BasicWindow runStringBlock = new PluralWindow("RunBlock",0,SizeY - 5,SizeX,5);
            runStringBlock.AddChildWindow(new RunningStringWindow("RunStr",0, SizeY - 5, SizeX, 5));

            mBasicWindow.AddChildWindow(mWindowDesigner.CreateWindow("EventForm", (int)FormWindowProp.PosX, (int)FormWindowProp.PosY,
                (int)FormWindowProp.Width, (int)FormWindowProp.Height));
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
            mWindowDesigner = new ShedulerWindowDesigner();

            DesignConsole();
        }

        public void MainLoop()
        {
            mWindowDrawer.InitialDrawing();
            while(true)
            {
                if (Console.KeyAvailable)
                    mActiveWindow.KeyReact(Console.ReadKey(true),ref mActiveWindow);
                mWindowDrawer.Draw();
            }
        }

    }
}
