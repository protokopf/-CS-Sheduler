using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.ExtraTypes.EventArgsTypes
{
    public abstract class ActionEventArgs : EventArgs
    {
        abstract public Dictionary<string, string> Storage
        {
            get;
            set;
        }
    }
}
