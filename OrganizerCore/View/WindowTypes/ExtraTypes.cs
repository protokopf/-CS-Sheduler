using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.WindowTypes
{
    public class ActionEventArgs : EventArgs
    {
        public Dictionary<string, string> Storage { get; set; }
        public void AllocateStorage()
        {
            Storage = new Dictionary<string, string>();
        }
    }

    public enum Border : int
    {
        LeftUpCorner = '\x250c',
        RightUpCorner = '\x2510',
        LeftDownCorner = '\x2514',
        RightDownCorner = '\x2518',
        UpBorder = '\x2500',
        DownBorder = '\x2500',
        LeftBorder = '\x2502',
        RightBorder = '\x2502',
        Space = '\x0020'
    }
    public enum ConsoleProperties : int
    {
        Height = 78,
        Width = 69
    }
}
