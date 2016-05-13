using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class InputWindow : BasicWindow, IDrawable
    {
        private StringBuilder mBuilder;

        private int mInitPosition = 0;
        private int yPos;

        private void AddChar(char c)
        {
            mBuilder.Append(c);
            if (mBuilder.Length > Width - 2)
                ++mInitPosition;
            WinHasChanged();
        }
        private void RemoveChar()
        {
            if (mBuilder.Length >= 1)
            {
                mBuilder.Remove(mBuilder.Length - 1, 1);
                if (mBuilder.Length < Width - 2)
                    mInitPosition = 0;
                WinHasChanged();
            }
        }

        public InputWindow(string name, int x, int y, int w, int h) : base(name,x,y,w,h)
        {
            mBuilder = new StringBuilder();
            yPos = PositionY + Height / 2;
        }

        void IDrawable.Draw()
        {
            base.Draw();
            for(int i = 1, index = mInitPosition; i < Width - 1 && index < mBuilder.Length; ++i, ++index)
            {
                Console.SetCursorPosition(PositionX + i,yPos);
                Console.Write(mBuilder[index]);
            }
        }
        void IDrawable.Clean()
        {
            for (int i = 1; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(PositionX + i, yPos);
                Console.Write(' ');
            }
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void Action()
        {
            ActionEventArgs e = new ActionEventArgs();
            e.Storage.Add(Name, mBuilder.ToString());
            base.OnAction(e);
        }
        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
                    break;
                case ConsoleKey.Enter:
                    Action();
                    GoToParent(ref activeWindow);
                    break;
                case ConsoleKey.Backspace:
                    RemoveChar();
                    break;
                default:
                    AddChar(key.KeyChar);
                    break;

            }
        }

        public override string ToString()
        {
            return mBuilder.ToString();
        }
    }
}
