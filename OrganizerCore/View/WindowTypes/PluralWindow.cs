using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.WindowTypes
{
    public class PluralWindow : BasicWindow
    {
        private int mCurrentWindowIndex = 0;
        private void SlideNextWindow()
        {
            if (Childs.Count != 0)
            {
                Childs[mCurrentWindowIndex].OutFocus();
                while (true)
                {
                    mCurrentWindowIndex = (mCurrentWindowIndex == Childs.Count) ? (0) : (mCurrentWindowIndex + 1);
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

        public override void Draw()
        {
            base.Draw();
            foreach (var wind in Childs)
                if(!wind.IsHidden)
                    wind.Draw();
        }
        public override void KeyReact(ConsoleKey key, BasicWindow activeWindow)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    SlideNextWindow();
                    break;
                case ConsoleKey.DownArrow:
                    SlidePrevWindow();
                    break;
                case ConsoleKey.Enter:
                    activeWindow = Childs[mCurrentWindowIndex];
                    break;
            }
        }

        public override void Action()
        {
            ActionEventArgs e = new ActionEventArgs();
            OnAction(e);
        }
    }
}
