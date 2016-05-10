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

namespace OrganizerCore
{
    class Program
    {
        public static void Main(string[] args)
        {
            XDocument document = XDocument.Load("packages.xml");
            var packages = document.XPathSelectElements("packages/package/.");
            foreach(XElement nav in packages)
            {
                Console.WriteLine(nav.ToString());
                Console.WriteLine("------------------------------------------");
            }
            
        }
    }
}
