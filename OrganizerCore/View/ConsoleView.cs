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

            BasicWindow mBasicWindow = new PluralWindow("BasicWindow", 0, 0, SizeX, SizeY);

            BasicWindow timeBlock = new CurrentTimeWindow("TimeWindow", SizeX - 25, 0, 25, 3);

            BasicWindow listTitle = new TextWindow("EVENT LIST", 32, 1);
            BasicWindow listBox = new ListBoxWindow("ListBoxWindow", 1, 2, 73, 17);

            mBasicWindow.AddChildWindow(mWindowDesigner.CreateWindow("SideBlock", SizeX - 25, 11, 25, 34));
            mBasicWindow.AddChildWindow(mWindowDesigner.CreateWindow("EventForm", 5, 5, 40, 35));
            mBasicWindow.AddChildWindow(mWindowDesigner.CreateWindow("MessageBlock", 0, SizeY - 5, SizeX, 5));

            mBasicWindow.AddChildWindow(listTitle);
            mBasicWindow.AddChildWindow(listBox);
            mBasicWindow.AddChildWindow(timeBlock);

            mActiveWindow = mBasicWindow;

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
