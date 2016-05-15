using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class ListBoxWindow : BasicWindow, IDrawable
    {
        private int nextItemYPosition = 0;

        private int mCurrentIndex = 0;

        private int mItemCapacity;

        private void MoveListItem()
        {
            foreach(var child in Childs)
                child.ShowWindow(false);
            BasicWindow childW = null;
            for(int i = 0, index = mCurrentIndex, y = 1; i < mItemCapacity; ++i,++index, y+=3)
            {
                childW = Childs[index];
                childW.PositionY = PositionY + y;
                childW.ShowWindow(true);
                childW.WinHasChanged();
            }
            this.WinHasChanged();
        }        

        private void NextItem()
        {
            int childsCount = Childs.Count;
            if(childsCount > 1)
            {
                if (mCurrentWindowIndex < childsCount - 1)
                {
                    Childs[mCurrentWindowIndex].OutFocus();
                    Childs[++mCurrentWindowIndex].InFocus();
                }
            }

            int innerCursor = mCurrentWindowIndex - mCurrentIndex;
            if(innerCursor >= mItemCapacity)
            {
                ++mCurrentIndex;
                MoveListItem();
            }
        }
        private void PreviousItem()
        {
            int childsCount = Childs.Count;
            if (childsCount > 1)
            {
                if (mCurrentWindowIndex > 0)
                {
                    Childs[mCurrentWindowIndex].OutFocus();
                    Childs[--mCurrentWindowIndex].InFocus();
                }
            }

            //int innerCursor = mCurrentWindowIndex - mCurrentIndex;
            if (mCurrentWindowIndex == mCurrentIndex - 1 && mCurrentIndex > 0)
            {
                --mCurrentIndex;
                MoveListItem();
            }
        }

        public ListBoxWindow(string name, int x, int y, int w, int h) : base(name,x,y,w,h)
        {
            mItemCapacity = (Height - 2) / 3;
            WinHasChanged();
        }

        void IDrawable.Draw()
        {
            base.Draw();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    PreviousItem();
                    break;
                case ConsoleKey.DownArrow:
                    NextItem();
                    break;
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
                    break;
            }
        }
        public override void AddChildWindow(BasicWindow chWindow)
        {
            base.AddChildWindow(chWindow);

            chWindow.PositionX = PositionX + 1;
            chWindow.PositionY = PositionY + 1 + nextItemYPosition;
            chWindow.Width = Width - 2;

            if (Childs.Count >= mItemCapacity)
                chWindow.ShowWindow(false);

            nextItemYPosition += chWindow.Height;
        }
    }
}
