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
        private BasicWindow mActiveWindow = null;

        private IWindowDrawer mWindowDrawer;
        private IWindowDesigner mWindowDesigner;
        private IWindowHandler mWindowHandler;

        private void DesignConsole()
        {
            Console.CursorVisible = false;
            Console.SetBufferSize(SizeX, SizeY);
            Console.WindowHeight = SizeY + 1;
            Console.WindowWidth = SizeX + 1;
            Console.Title = "Sheduler";

            mActiveWindow = new PluralWindow("BasicWindow", 0, 0, SizeX, SizeY);

            mActiveWindow.AddChildWindow(new TextWindow("EVENT LIST", 32, 1));
            mActiveWindow.AddChildWindow(new ListBoxWindow("ListBoxWindow", 1, 2, 73, 17));
            mActiveWindow.AddChildWindow(new CurrentTimeWindow("TimeWindow", SizeX - 25, 0, 25, 3));
            mActiveWindow.AddChildWindow(mWindowDesigner.CreateWindow("SideBlock", SizeX - 25, 11, 25, 34));
            mActiveWindow.AddChildWindow(mWindowDesigner.CreateWindow("MessageBlock", 0, SizeY - 5, SizeX, 5));

            mActiveWindow.AddChildWindow(mWindowDesigner.CreateWindow("EventForm", 15, 20, 40, 20));
            
            mWindowDrawer.CatchAllChild(mActiveWindow);
            mWindowHandler.CatchAllChild(mActiveWindow);
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

            mWindowDrawer = new WindowDrawer();
            mWindowDesigner = new ShedulerWindowDesigner();
            mWindowHandler = new WindowHandler();

            DesignConsole();
        }

        public void MainLoop()
        {
            mWindowDrawer.InitialDraw();
            while(true)
            {
                if (Console.KeyAvailable)
                    mActiveWindow.KeyReact(Console.ReadKey(true),ref mActiveWindow);
                mWindowDrawer.Draw();
            }
        }

    }
}
