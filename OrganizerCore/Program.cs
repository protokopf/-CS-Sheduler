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

using OrganizerCore.Presenter;

using IBaseable = OrganizerCore.Model.ModelCore.IModelBaseCommunicator;

namespace OrganizerCore
{
    class Program
    {

        public static void Main(string[] args)
        {
            ConsoleView view = new ConsoleView((int)ConsoleProperties.Width, (int)ConsoleProperties.Height);
            IBaseable   xmlBase = new XMLBase();
            IEqualityComparer<Package> comparer = new FullEqualityComparer();
            ModelCore model = new ModelCore(xmlBase, comparer);

            ConsolePresenter presenter = new ConsolePresenter(model, view);
            presenter.Proceed();
        }
    }
}
