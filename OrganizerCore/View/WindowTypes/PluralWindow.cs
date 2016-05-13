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
        private void SlideNextWindow()
        {
            if (Childs.Count != 0)
            {
                Childs[mCurrentWindowIndex].OutFocus();
                while (true)
                {
                    mCurrentWindowIndex = (mCurrentWindowIndex == Childs.Count - 1) ? (0) : (mCurrentWindowIndex + 1);
                    if (Childs[mCurrentWindowIndex].IsHidden)
                        continue;
                    Childs[mCurrentWindowIndex].InFocus();                    
                    break;
                }
            }
        }
        private void SlidePrevWindow()
        {
            if (Childs.Count != 0)
            {
                Childs[mCurrentWindowIndex].OutFocus();
                while (true)
                {
                    mCurrentWindowIndex = (mCurrentWindowIndex == 0) ? (Childs.Count - 1) : (mCurrentWindowIndex - 1);
                    if (Childs[mCurrentWindowIndex].IsHidden)
                        continue;
                    Childs[mCurrentWindowIndex].InFocus();
                    break;
                }
            }
        }

        public PluralWindow() : base()
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
                case ConsoleKey.UpArrow:
                    SlideNextWindow();
                    break;
                case ConsoleKey.DownArrow:
                    SlidePrevWindow();
                    break;
                case ConsoleKey.Enter:
                    if (Childs.Count != 0)
                    {
                        activeWindow = Childs[mCurrentWindowIndex];
                        activeWindow.OutFocus();
                    }
                    break;
                case ConsoleKey.Escape:
                    if (this.Parent != null)
                    {
                        foreach (var child in Childs)
                            child.OutFocus();
                        activeWindow = this.Parent;
                        activeWindow.Childs[activeWindow.CurrentWindowIndex].InFocus();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
