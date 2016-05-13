using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class RunningStringWindow : BasicWindow, IDrawable
    {
        private Timer mTimer;
        private const string mDefaultString = "There is no messages";
        private string Label { get; set; }

        private int mFirst;
        private int mLast;
        private int mInnerIndex;

        private void MoveStringAlongWindow(object source, ElapsedEventArgs e)
        {
            WinHasChanged();
            if(mLast == 0)
            {
                mFirst = Width;
                mLast = Width + Label.Length - 1;
                mInnerIndex = 0;
                return;
            }
            if(mFirst == 0)
            {
                ++mInnerIndex;
                --mLast;
                return;
            }
            --mFirst;
            --mLast;
        }

        public RunningStringWindow(int x, int y, int w, int h) : base(x, y, w, h)
        {
            Label = mDefaultString;

            mFirst = Width;
            mLast = Width + Label.Length - 1;
            mInnerIndex = 0;

            mTimer = new Timer(100);
            mTimer.Elapsed += MoveStringAlongWindow;
            mTimer.Start();
        }

        void IDrawable.Draw()
        {
            int yPos = (Height / 2) + PositionY;
            for(int i = mFirst, cursor = mInnerIndex; i < Width - 1 && i < mLast; ++i, ++cursor)
            {
                Console.SetCursorPosition(PositionX + ((i==0)?1:i), yPos);
                Console.Write(Label[cursor]);
            }
        }
        void IDrawable.Clean()
        {
            int yPos = (Height / 2) + PositionY;
            for (int i = mFirst; i < Width - 1; ++i)
            {
                Console.SetCursorPosition(((i == 0) ? 1 : i), yPos);
                Console.Write(' ');
            }
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {
 
        }
        public override void FromParentAction(ref BasicWindow activeWindow)
        {
            if (Label != mDefaultString)
                Label = mDefaultString;
            this.IsWindowChanged = true;
        }
        public override void ReactMethod(object sender, ActionEventArgs e)
        {
            Label = e.Storage["Message"];
            this.IsWindowChanged = true;
        }
    }
}
