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
            int initPositionX = (Width - Caption.Length) / 2;
            int initPositionY = Height / 2;
            Console.SetCursorPosition(PositionX + initPositionX,PositionY + initPositionY);
            Console.Write(Caption);
        }

        public String Caption { get; set; }
        public ButtonWindow(string caption) : base()
        {
            Caption = caption;
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
                case ConsoleKey.Enter:
                    Action();
                    break;
            }
        }
    }
}
