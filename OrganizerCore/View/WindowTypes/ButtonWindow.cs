using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class ButtonWindow : BasicWindow, IDrawable
    {
        private int mCapX;
        private int mCapY;

        private string mClearString;

        protected void DrawCaption()
        {            
            Console.SetCursorPosition(PositionX + mCapX,PositionY + mCapY);
            Console.Write(Name);
        }
        protected void ClearCaption()
        {
            Console.SetCursorPosition(PositionX + mCapX, PositionY + mCapY);
            Console.Write(mClearString);
        }


        public ButtonWindow(string caption, int x, int y, int w, int h) : base(caption,x,y,w,h)
        {
            mCapX = (Width - Name.Length) / 2;
            mCapY = Height / 2;
            mClearString = new string(' ', Name.Length);
        }

        void IDrawable.Draw()
        {
            base.Draw();
            DrawCaption();
        }
        void IDrawable.Clean()
        {
            base.Clean();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
                    break;
            }
        }
        public override void FromParentAction(ref BasicWindow activeWindow)
        {
           
        }
    }
}
