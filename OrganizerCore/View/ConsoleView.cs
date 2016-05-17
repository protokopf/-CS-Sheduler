using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using OrganizerCore.View.WindowTypes;
using OrganizerCore.View.ExtraTypes;

using OrganizerCore.Presenter.Commands;

namespace OrganizerCore.View
{
    class ConsoleView
    {
        public delegate RetType CommandHandler<Arg,RetType> (Arg argument);

        private bool mListHasChanged = false;
        private ActionEventArgs mListEvent;

        public event CommandHandler<ICommand,ActionEventArgs> ConsoleCommands;

        private Dictionary<string,string> mVisibleEvents;
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
        private void BindWindowsWithMethods()
        {
            mWindowHandler["BasicWindow.SideBlock.ADD"].WinEvent += ShowEventForm;
            mWindowHandler["BasicWindow.EventForm"].WinEvent += PostEventForm;
        }

        private ActionEventArgs OnCommand(ICommand command)
        {
            return ConsoleCommands.Invoke(command);
        }

        private void ShowEventForm(object sender, ActionEventArgs e)
        {
            BasicWindow form = mWindowHandler["BasicWindow.EventForm"];
            form.ReactMethod(this, e);
            form.ShowWindow(true);
            mActiveWindow = form;
        }

        private void PostEventForm(object sender, ActionEventArgs e)
        {
            ICommand addCommand = new AddEventCommand(e);
            OnCommand(addCommand);
            mListHasChanged = true;
        }
        private void DeleteEve

        private void UpdateEventList()
        {
            BasicWindow listBox = mWindowHandler["BasicWindow.ListBoxWindow"];
            ICommand updateCommand = new UpdateListCommand();

            mWindowDrawer.CutParentWindow(listBox);
       
            ActionEventArgs e = OnCommand(updateCommand);

            listBox.ReactMethod(this, e);
            mWindowDrawer.CompleteParentWindow(listBox);
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

            mVisibleEvents = new Dictionary<string,string>();
            DesignConsole();
        }

        public void MainLoop()
        {
            UpdateEventList();
            BindWindowsWithMethods();
            mWindowDrawer.InitialDraw();
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Delete)
                        break;
                    mActiveWindow.KeyReact(key, ref mActiveWindow);
                }
                SafeAddMessage();
                mWindowDrawer.Draw();
            }
        }

        public void AddMessageInQueue(string message)
        {
            mListEvent = new ActionEventArgs();
            mListEvent.Storage.Add("Message", message);
            mListHasChanged = true;
        }

        private void SafeAddMessage()
        {
            if(mListHasChanged)
            {
                mListHasChanged = false;
                if (mListEvent != null)
                {
                    mWindowHandler["BasicWindow.MessageBlock.RunString"].ReactMethod(this, mListEvent);
                    mListEvent = null;
                }
                UpdateEventList();
            }
        }
    }
}
