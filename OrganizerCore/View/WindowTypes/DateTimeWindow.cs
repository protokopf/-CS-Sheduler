using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.View.WindowTypes
{
    class DateTimeWindow : PluralWindow, IDrawable
    {
        private StringBuilder mBuilder;
        private string TryParseDateTime()
        {

        }
        public DateTimeWindow(string name, int x, int y, int w, int h) : base(name,x,y,w,h)
        {
            mBuilder = new StringBuilder();

            AddChildWindow(new CounterWindow("hour", PositionX + 1, PositionY + 2, 4, 3, 23, 0));
            AddChildWindow(new CounterWindow("min", PositionX + 1 + 4, PositionY + 2, 4, 3, 59, 0));
            AddChildWindow(new CounterWindow("sec", PositionX + 1 + 8, PositionY + 2, 4, 3, 59, 0));

            AddChildWindow(new CounterWindow("year", PositionX + 1 + 14, PositionY + 2, 6, 3, 2020, 2015));
            AddChildWindow(new CounterWindow("mon", PositionX + 1 + 20, PositionY + 2, 4, 3, 12, 1));
            AddChildWindow(new CounterWindow("day", PositionX + 1 + 24, PositionY + 2, 4, 3, 31, 1));
        }
    }
}
