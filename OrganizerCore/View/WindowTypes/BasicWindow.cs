using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    public abstract class BasicWindow : IDrawable
    {
        protected int mCurrentWindowIndex = 0;
        public int CurrentWindowIndex
        {
            get { return mCurrentWindowIndex; }
        }

        public string Name { get; set; }

        public event EventHandler<ActionEventArgs> WinEvent;

        public BasicWindow(string name, int x, int y, int w, int h)
        {
            Name = name;

            PositionX = x;
            PositionY = y;
            Width = w;
            Height = h;

            Childs = new List<BasicWindow>();
            BackgroundColor = ConsoleColor.White;
            FontColor = ConsoleColor.Black;

            IsHidden = false;
            IsInteractable = true;
        }
        public void AddChildWindow(BasicWindow chWindow)
        {
            Childs.Add(chWindow);
            chWindow.Parent = this;
        }
        public void GoToParent(ref BasicWindow activeWindow)
        {
            if (this.Parent != null)
            {
                foreach (var child in Childs)
                    child.OutFocus();
                activeWindow = this.Parent;
                activeWindow.Childs[activeWindow.CurrentWindowIndex].InFocus();
            }
        }
        public void WinHasChanged()
        {
            IsWindowChanged = true;
        }

        public BasicWindow Parent { get; set; }
        public List<BasicWindow> Childs { get; set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor FontColor { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width     { get; set; }
        public int Height    { get; set; }

        public bool IsHidden { get; set; }
        public bool IsWindowChanged { get; set; }
        public bool IsInteractable { get; set; }

        // for IDrawable
        public  void Draw()
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
                    else if ((i == PositionY || i == maxY - 1 )&& j > PositionX)
                        symbol = (char)Border.UpBorder;
                    else if (i > PositionY && (j == PositionX || j == maxX - 1))
                        symbol = (char)Border.LeftBorder;
                    else
                    {
                        //symbol = (char)Border.Space;
                        continue;
                    }
                    Console.Write(symbol);
                }
            }
            Console.ResetColor();
        }
        public  void Clean()
        {
            int maxY = PositionY + Height;
            int maxX = PositionX + Width;
            for (int i = PositionY; i < maxY; ++i)
            {
                for (int j = PositionX; j < maxX; ++j)
                {
                    if (i == PositionY || j == PositionX || i == maxY - 1 || j == maxX - 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                }
            }
            Console.ResetColor();
        }
        public  bool IsChanged()
        {
            bool changed = IsWindowChanged;
            if (changed)
                IsWindowChanged = false;
            return changed;
        }

        public virtual void OnAction(ActionEventArgs e)
        {
            if (WinEvent != null)
                WinEvent.Invoke(this, e);
        }
        public virtual void Action()
        {
            OnAction(null);
        }

        public virtual void FromParentAction(ref BasicWindow activeWindow)
        {
            activeWindow = this;
            activeWindow.OutFocus();
        }
        public virtual void ReactMethod(object sender, ActionEventArgs e)
        {

        }

        public virtual void OutFocus()
        {
            BackgroundColor = ConsoleColor.White;
            WinHasChanged();
        }
        public virtual void InFocus()
        {
            BackgroundColor = ConsoleColor.Blue;
            WinHasChanged();
        }

        public abstract void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow);
    }
}
