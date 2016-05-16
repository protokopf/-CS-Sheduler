using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.ExtraTypes
{

    public class ActionEventArgs : EventArgs
    {
        private Dictionary<string, string> mStorage = new Dictionary<string, string>();
        public Dictionary<string, string> Storage
        {
            get { return mStorage; }
            set { mStorage = value; }
        }
    }


    public enum Border : int
    {
        LeftUpCorner    = '\x250c',
        RightUpCorner   = '\x2510',
        LeftDownCorner  = '\x2514',
        RightDownCorner = '\x2518',
        UpBorder        = '\x2500',
        DownBorder      = '\x2500',
        LeftBorder      = '\x2502',
        RightBorder     = '\x2502',
        Space           = '\x0020'
    }
    public enum ConsoleProperties : int
    {
        Height = 50,
        Width = 100
    }
    public enum WindowSymbols : int
    {
        ArrowUp = '\x25b2',
        ArrowDown = '\x25bc'
    }
}
