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
        private DateTime mDateTime;

        private string TryParseDateTime()
        {
            mBuilder.Append(Childs[0] + "." + Childs[1] + "." + Childs[2] + " ");
            mBuilder.Append(Childs[3] + ":" + Childs[3] + ":" + Childs[4]);
            return (DateTime.TryParse(mBuilder.ToString(),out mDateTime)) ? mDateTime.ToString() : null;
        }

        public DateTimeWindow(string name, int x, int y, int w = 30, int h = 6) : base(name,x,y,w,h)
        {
            mBuilder = new StringBuilder();

            AddChildWindow(new TextWindow("day", PositionX + 1 + 24, PositionY + 1));
            AddChildWindow(new CounterWindow("day", PositionX + 1 + 24, PositionY + 2, 4, 3, 31, 1));

            AddChildWindow(new TextWindow("mon", PositionX + 1 + 20, PositionY + 1));
            AddChildWindow(new CounterWindow("mon", PositionX + 1 + 20, PositionY + 2, 4, 3, 12, 1));

            AddChildWindow(new TextWindow("year", PositionX + 1 + 15, PositionY + 1));
            AddChildWindow(new CounterWindow("year", PositionX + 1 + 14, PositionY + 2, 6, 3, 2020, 2015));

            AddChildWindow(new TextWindow("hour", PositionX + 1, PositionY + 1));
            AddChildWindow(new CounterWindow("h", PositionX + 1, PositionY + 2, 4, 3, 23, 0));

            AddChildWindow(new TextWindow("min", PositionX + 1 + 5, PositionY + 1));
            AddChildWindow(new CounterWindow("min", PositionX + 1 + 4, PositionY + 2, 4, 3, 59, 0));

            AddChildWindow(new TextWindow("sec", PositionX + 1 + 4 + 5, PositionY + 1));
            AddChildWindow(new CounterWindow("sec", PositionX + 1 + 8, PositionY + 2, 4, 3, 59, 0));
        }

        public override string ToString()
        {
            return TryParseDateTime();
        }
    }
}
