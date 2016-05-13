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
        private void DrawCaption()
        {            
            int initPositionX = (Width - Name.Length) / 2;
            int initPositionY = Height / 2;
            Console.SetCursorPosition(PositionX + initPositionX,PositionY + initPositionY);
            Console.Write(Name);
        }

        public ButtonWindow(string caption, int x, int y, int w, int h) : base(caption,x,y,w,h)
        {
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
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
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
