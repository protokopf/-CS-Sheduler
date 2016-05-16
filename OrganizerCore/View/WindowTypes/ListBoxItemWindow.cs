using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class ListBoxItemWindow : BasicWindow, IDrawable
    {
        private int mInitPosition = 0;

        private void LeftMove()
        {
            if (Name.Length >= 1)
            {
                if (mInitPosition > 0)
                    --mInitPosition;
                WinHasChanged();
            }
        }
        private void RightMove()
        {
            if (Name.Length - mInitPosition > Width - 2)
                ++mInitPosition;
            WinHasChanged();
        }

        public ListBoxItemWindow(string name, int h = 3)
        {
            Name = name;
            Height = h;
        }

        void IDrawable.Draw()
        {
            base.Draw();
            for (int i = 1, index = mInitPosition; i < Width - 1 && index < Name.Length; ++i, ++index)
            {
                Console.SetCursorPosition(PositionX + i, PositionY + 1);
                Console.Write(Name[index]);
            }
        }
        void IDrawable.Clean()
        {
            //base.Clean();
            for(int i = 1; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(PositionX + i, PositionY + 1);
                Console.Write(' ');
            }
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch(key.Key)
            {
                case ConsoleKey.LeftArrow:
                    LeftMove();
                    break;
                case ConsoleKey.RightArrow:
                    RightMove();
                    break;
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
                    break;
            }
        }

    }
}
