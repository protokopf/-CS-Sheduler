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

        private void Increment()
        {
            mValue = (mValue < maxValue) ? mValue + 1 : minValue;
            WinHasChanged();
        }
        private void Decrement()
        {
            mValue = (mValue > minValue) ? mValue - 1 : maxValue;
            WinHasChanged();
        }

        public CounterWindow(string name,int x, int y, int w, int h, int max, int min = 0) : base(name,x,y,w,h)
        {
            maxValue = max;
            minValue = mValue =  min;
            yPos = PositionY + Height / 2;
        }

        void IDrawable.Draw()
        {
            base.Draw();
            Console.SetCursorPosition(PositionX + 1, yPos);
            Console.Write(mValue.ToString());
            Console.BackgroundColor = BackgroundColor;
            Console.SetCursorPosition(PositionX + Width - 2, PositionY);
            Console.Write((char)WindowSymbols.ArrowUp);
            Console.SetCursorPosition(PositionX + Width - 2, PositionY + Height - 1);
            Console.Write((char)WindowSymbols.ArrowDown);
            Console.ResetColor();
        }
        void IDrawable.Clean()
        {
            for(int i = 0; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(PositionX + i, yPos);
                Console.Write(' ');
            }
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
                    GoToParent(ref activeWindow);
                    break;
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
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
            e.Storage.Add(Name,mValue.ToString());
            OnAction(e);
        }

        public override string ToString()
        {
            return mValue.ToString();
        }
    }
}
