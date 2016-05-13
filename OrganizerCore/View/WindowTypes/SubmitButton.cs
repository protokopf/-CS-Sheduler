using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;
namespace OrganizerCore.View.WindowTypes
{
    class SubmitButton : ButtonWindow, IDrawable
    {
        public SubmitButton(string name, int x, int y, int w, int h)
            : base(name, x, y, w, h)
        {

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
            Parent.Action();
            this.OutFocus();
            Parent.GoToParent(ref activeWindow);
        }
    }
}
