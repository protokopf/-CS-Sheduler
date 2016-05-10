using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.WindowTypes
{
    public abstract class BasicWindow
    {
        public BasicWindow()
        {
            Childs = new List<BasicWindow>();
        }

        public event EventHandler<ActionEventArgs> Event;
        public BasicWindow Parent { get; set; }
        public List<BasicWindow> Childs { get; set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor FontColor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width     { get; set; }
        public int Height    { get; set; }
        public bool IsHidden { get; set; }

        
        public abstract string GetData();

        public abstract void Action();
        public abstract void KeyReact(ConsoleKey key, BasicWindow activeWindow);

        protected virtual void OnAction(ActionEventArgs e)
        {
            if(Event != null)
                Event.Invoke(this,e);
        }
        public    virtual void Draw()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = FontColor;
            int maxY = PositionY + Height;
            int maxX = PositionX + Width;
            for(int i = PositionY; i < maxY; ++i)
            {
                for(int j = PositionX; j < maxX; ++j)
                {
                    char symbol;
                    Console.SetCursorPosition(j,i);
                    if (i == PositionY && j == PositionX)
                        symbol = (char)Border.LeftUpCorner;
                    else if (i == PositionY && j == maxX - 1)
                        symbol = (char)Border.RightUpCorner;
                    else if (i == maxY - 1 && j == PositionX)
                        symbol = (char)Border.LeftDownCorner;
                    else if (i == maxY - 1 && j == maxX - 1)
                        symbol = (char)Border.RightDownCorner;
                    else if (i == PositionY && j > PositionX)
                        symbol = (char)Border.UpBorder;
                    else if (i == maxY - 1 && j > PositionX)
                        symbol = (char)Border.DownBorder;
                    else if (i > PositionY && j == PositionX)
                        symbol = (char)Border.LeftBorder;
                    else if (i > PositionY && j == maxX - 1)
                        symbol = (char)Border.RightBorder;
                    else
                        symbol = (char)Border.Space;
                    Console.Write(symbol);
                }
            }
            Console.ResetColor();
        }
        public    virtual void AddChildWindow(BasicWindow chWindow)
        {
            Childs.Add(chWindow);
            chWindow.Parent = this;
        }

        public    virtual void OutFocus()
        {
            BackgroundColor = ConsoleColor.White;
        }
        public    virtual void InFocus()
        {
            BackgroundColor = ConsoleColor.Cyan;
        }
    }
}
