using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.WindowTypes;

namespace OrganizerCore.View.ExtraTypes
{
    interface IWindowDesigner
    {
        BasicWindow CreateWindow(string windowClass, int x, int y, int w, int h);
    }

    class ShedulerWindowDesigner : IWindowDesigner
    {
        private int GetOptimalWidth(BasicWindow parent, int leftStep)
        {
            int width = parent.Width - 2 * leftStep;
            return width;
        }

        private int GetCenterX(BasicWindow parent, int childWidth)
        {
            return parent.PositionX + ((parent.Width - childWidth) / 2);
        }
        private int GetCenterY(BasicWindow parent, int childHeight, ref int step)
        {
            int posY = parent.PositionY + step + 1;
            step += childHeight;
            return posY;
        }

        BasicWindow IWindowDesigner.CreateWindow(string windowClass, int x, int y, int w, int h)
        {
            switch(windowClass)
            {
                case "EventForm":
                    return CreateEventForm("EventForm", x, y, w, h);
            }
            return null;
        }

        private BasicWindow CreateEventForm(string name, int x, int y, int w, int h)
        {
            int step = 0;
            int inputWidth = 0;
            BasicWindow eventForm = new FormWindow(name, x, y, w, h);
            eventForm.Childs.Add(new TextWindow("TITLE", GetCenterX(eventForm, 5), GetCenterY(eventForm, 1, ref step)));
            inputWidth = GetOptimalWidth(eventForm, 2);
            eventForm.AddChildWindow(new InputWindow("Name", GetCenterX(eventForm, inputWidth), GetCenterY(eventForm, 3, ref step), inputWidth, 3));

            eventForm.AddChildWindow(new TextWindow("DESCRIPTION", GetCenterX(eventForm, 11), GetCenterY(eventForm, 1, ref step)));
            eventForm.AddChildWindow(new InputWindow("Description", GetCenterX(eventForm, inputWidth), GetCenterY(eventForm, 3, ref step), inputWidth, 3));

            eventForm.AddChildWindow(new TextWindow("Begin Date", GetCenterX(eventForm, 10), GetCenterY(eventForm, 1, ref step)));
            eventForm.AddChildWindow(new DateTimeWindow("BeginDate", GetCenterX(eventForm, 30), GetCenterY(eventForm, 6, ref step)));

            eventForm.AddChildWindow(new TextWindow("End Date", GetCenterX(eventForm, 10), GetCenterY(eventForm, 1, ref step)));
            eventForm.AddChildWindow(new DateTimeWindow("EndDate", GetCenterX(eventForm, 30), GetCenterY(eventForm, 6, ref step)));

            return eventForm;


        }
    }
}
