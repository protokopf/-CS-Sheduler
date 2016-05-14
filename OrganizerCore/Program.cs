using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using OrganizerCore.Model;
using OrganizerCore.PackageBases;
using OrganizerCore.View.WindowTypes;
using OrganizerCore.View;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore
{
    class Program
    {
        public static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView((int)ConsoleProperties.Width, (int)ConsoleProperties.Height);
            view.MainLoop();
        }
    }
}
