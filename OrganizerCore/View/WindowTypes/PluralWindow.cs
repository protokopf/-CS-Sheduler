using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.WindowTypes
{
    public class PluralWindow : BasicWindow
    {
        private int mCurrentWindowIndex;
        private void SlideNextWindow()
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
        private void SlidePrevWindow()
        {
            Childs[mCurrentWindowIndex].OutFocus();
            mCurrentWindowIndex = (mCurrentWindowIndex == 0) ? (Childs.Count) : (mCurrentWindowIndex - 1);
            Childs[mCurrentWindowIndex].InFocus();
        }

        public PluralWindow() : base()
        {
            Childs[mCurrentWindowIndex = 0].InFocus();
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
    }
}
