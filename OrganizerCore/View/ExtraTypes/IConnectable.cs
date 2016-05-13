using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.ExtraTypes
{
    interface IConnectable
    {
        void OnAction(ActionEventArgs e);
        void Action();

        void ReactMethod(object sender, ActionEventArgs e);
    }
}
