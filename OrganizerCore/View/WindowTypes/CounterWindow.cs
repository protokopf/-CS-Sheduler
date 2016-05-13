using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class CounterWindow : BasicWindow, IDrawable
    {
        private readonly int maxValue;
        private readonly int minValue;
        private int mValue;
        private int yPos;

        private readonly string mName;

        private void Increment()
        {
            if (mValue < maxValue)
            {
                ++mValue;
                this.IsWindowChanged = true;
            }
        }
        private void Decrement()
        {
            if (mValue > 0)
            {
                --mValue;
                this.IsWindowChanged = true;
            }
        }

        public CounterWindow(string name,int x, int y, int w, int h, int max, int min = 0) : base(x,y,w,h)
        {
            mName = name;
            maxValue = max;
            minValue = mValue =  min;
            yPos = PositionY + Height / 2;
        }

        void IDrawable.Draw()
        {
            base.Draw();
            Console.SetCursorPosition(PositionX + Width - 2, PositionY);
            Console.Write((char)WindowSymbols.ArrowUp);
            Console.SetCursorPosition(PositionX + Width - 2, PositionY + Height - 1);
            Console.Write((char)WindowSymbols.ArrowDown);
            Console.SetCursorPosition(PositionX + 1, yPos);
            Console.Write(mValue.ToString());
        }
        void IDrawable.Clean()
        {
            for(int i = 0; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(PositionX + i, yPos);
                Console.Write(' ');
            }
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    Increment();
                    break;
                case ConsoleKey.DownArrow:
                    Decrement();
                    break;
                case ConsoleKey.Enter:
                    Action();
                    break;
                case ConsoleKey.Escape:
                    if (this.Parent != null)
                    {
                        foreach (var child in Childs)
                            child.OutFocus();
                        activeWindow = this.Parent;
                        activeWindow.Childs[activeWindow.CurrentWindowIndex].InFocus();
                    }
                    break;

            }
        }
        public override void FromParentAction(ref BasicWindow activeWindow)
        {
            activeWindow = this;
        }
        public override void Action()
        {
            ActionEventArgs e = new ActionEventArgs();
            e.Storage.Add(mName,mValue.ToString());
            OnAction(e);
        } 
    }
}
