using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class RunningStringWindow : BasicWindow, IDrawable
    {
        private const string mDefaultString = "There is no messages";
        private string Label { get; set; }

        private void MoveStringAlongWindow()
        {

        }


        public RunningStringWindow() : base()
        {

        }

        void IDrawable.Draw()
        {
            // тут будет отрисовка строки в середине окна
        }
        void IDrawable.Clean()
        {
            // тут строка будет очищаться
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch(key.Key)
            {
                case ConsoleKey.Enter:
                    Label = mDefaultString;
                    break;
            }
        }
        public override void Action()
        {
            MoveStringAlongWindow();
            this.IsWindowChanged = true;
        }
    }
}
