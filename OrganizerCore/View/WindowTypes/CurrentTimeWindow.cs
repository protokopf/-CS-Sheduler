using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class CurrentTimeWindow : BasicWindow, IDrawable
    {
        private Timer mTimer;
        private DateTime mTime;
        private StringBuilder mBuilder;

        private int xPos = 5;
        private int yPos = 5;

        private void ChangeTime(object source, ElapsedEventArgs e)
        {
            mTime = DateTime.Now;
            WinHasChanged();
        }

        public CurrentTimeWindow(int x, int y,int w, int h)
            : base(x,y,w,h)
        {
            mBuilder = new StringBuilder();
            mTimer = new Timer(1000);
            mTimer.Elapsed += ChangeTime;
            mTimer.Start();

            yPos = PositionY + Height/2;
            xPos = PositionX + ((Width - mTime.ToString().Length) / 2);
        }

        void IDrawable.Draw()
        {
            mBuilder.Append(mTime.ToString());
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(mBuilder.ToString());
        }
        void IDrawable.Clean()
        {
            for (int i = xPos; i < mBuilder.Length; ++i)
            {
                Console.SetCursorPosition(xPos, yPos);
                Console.Write(' ');
            }
            mBuilder.Clear();
        }
        bool IDrawable.IsChanged()
        {
            return base.IsChanged();
        }

        public override void KeyReact(ConsoleKeyInfo key, ref BasicWindow activeWindow)
        {

        }

    }
}
