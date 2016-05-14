using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    public class PluralWindow : BasicWindow, IDrawable
    {
        protected void SlideNextWindow()
        {
            if (Childs.Count != 0)
            {
                Childs[mCurrentWindowIndex].OutFocus();
                while (true)
                {
                    mCurrentWindowIndex = (mCurrentWindowIndex == Childs.Count - 1) ? (0) : (mCurrentWindowIndex + 1);
                    if (Childs[mCurrentWindowIndex].IsHidden || !Childs[mCurrentWindowIndex].IsInteractable)
                        continue;
                    Childs[mCurrentWindowIndex].InFocus();                    
                    break;
                }
            }
        }
        protected void SlidePrevWindow()
        {
            if (Childs.Count != 0)
            {
                Childs[mCurrentWindowIndex].OutFocus();
                while (true)
                {
                    mCurrentWindowIndex = (mCurrentWindowIndex == 0) ? (Childs.Count - 1) : (mCurrentWindowIndex - 1);
                    if (Childs[mCurrentWindowIndex].IsHidden || !Childs[mCurrentWindowIndex].IsInteractable)
                        continue;
                    Childs[mCurrentWindowIndex].InFocus();
                    break;
                }
            }
        }

        public PluralWindow(string name,int x, int y, int w, int h) : base(name,x,y,w,h)
        {
            if(Childs.Count != 0)
                Childs[mCurrentWindowIndex].InFocus();
        }

        void IDrawable.Draw()
        {
            base.Draw();
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
                case ConsoleKey.LeftArrow:
                    goto case ConsoleKey.DownArrow;
                case ConsoleKey.RightArrow:
                    goto case ConsoleKey.UpArrow;
                case ConsoleKey.UpArrow:
                    SlideNextWindow();
                    break;
                case ConsoleKey.DownArrow:
                    SlidePrevWindow();
                    break;
                case ConsoleKey.Enter:
                    if (Childs.Count != 0)
                        Childs[mCurrentWindowIndex].FromParentAction(ref activeWindow);
                    break;
                case ConsoleKey.Escape:
                    GoToParent(ref activeWindow);
                    break;
                default:
                    break;
            }
        }
    }
}
