using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class FormWindow : PluralWindow, IDrawable
    {
        public ActionEventArgs StoreInfoFromChild()
        {
            ActionEventArgs e = new ActionEventArgs();
            foreach(var child in Childs)
                if(child.IsInteractable && child.Name != "SUBMIT")
                    e.Storage.Add(child.Name, child.ToString());
            return e;
        }

        private void PlaceAllWindows()
        {

        }

        public FormWindow(string name, int x, int y, int w, int h) : base(name,x,y,w,h)
        {
            AddChildWindow(new SubmitButton("SUBMIT", PositionX + (Width - 10) / 2, PositionY + Height - 4, 10, 3));
        }


        
        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    SlidePrevWindow();
                    break;
                case ConsoleKey.DownArrow:
                    SlideNextWindow();
                    break;
                case ConsoleKey.Enter:
                    if (Childs.Count != 0)
                        Childs[mCurrentWindowIndex].FromParentAction(ref activeWindow);
                    break;
            }
        }
        public override void Action()
        {
            OnAction(StoreInfoFromChild());
        }
        public override void ReactMethod(object sender, ActionEventArgs e)
        {
            if(e != null)
            {

            }
        }

        public override bool ValidateWindow()
        {
            foreach (var child in Childs)
                if (child.ValidateWindow() == false)
                    return false;
            return true;
        }
    }
}
