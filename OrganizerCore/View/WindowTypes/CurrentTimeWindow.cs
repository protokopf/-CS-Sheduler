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

        private int xPos;
        private int yPos;

        private void ChangeTime(object source, ElapsedEventArgs e)
        {
            mTime = DateTime.Now;
        }

        public CurrentTimeWindow(int x, int y,int w, int h)
            : base(x,y,h,w)
        {
            mBuilder = new StringBuilder();
            mTimer = new Timer(1000);
            mTimer.Elapsed += ChangeTime;
            mTimer.Start();

            yPos = PositionY + (Height / 2);
            yPos = PositionX + ((Width - mTime.ToShortDateString().Length) / 2);
        }

        void IDrawable.Draw()
        {
            mBuilder.Append(mTime.ToShortTimeString());
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

        public override 

    }
}
