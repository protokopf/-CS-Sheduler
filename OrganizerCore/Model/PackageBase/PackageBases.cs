using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OrganizerCore.Model;

using IBaseable = OrganizerCore.Model.ModelCore.IModelBaseCommunicator;



namespace OrganizerCore.PackageBases
{
    public class XMLBase : IBaseable
    {
        private readonly string mPathToFile = @"packages.xml";

        private XmlDocument mDocument;

        private XmlNode mRoot;
        private bool CheckNodeAndPackage(XmlNode node, Package package)
        {
            bool equality = true;
            foreach (var chain in package.Dictionary)
                foreach (XmlNode child in node.ChildNodes)
                    if (chain.Key == child.Name)
                        equality = (chain.Value == child.InnerText);
            return equality;
        }
        private Package FromNodeToPackage(XmlNode node)
        {
            var newPackage = new Package();
            foreach (XmlNode childNode in node.ChildNodes)
                newPackage.Dictionary.Add(childNode.Name, childNode.InnerText);
            return newPackage;
        }

        public        XMLBase()
        {
        }

        void          IBaseable.OpenBase()
        {
            mDocument = new XmlDocument();
            mDocument.Load(mPathToFile);
            mRoot = mDocument.DocumentElement;
        }
        void          IBaseable.CloseBase()
        {
        }

        void          IBaseable.PushPackagesList(List<Package> pList) 
        {
            foreach (var package in pList)
                ((IBaseable)this).PushOnePackage(package);
            mDocument.Save(mPathToFile);
        }
        List<Package> IBaseable.PullPackagesList() 
        {
            var packages = new List<Package>();
            foreach (XmlNode node in mRoot)
                packages.Add(FromNodeToPackage(node));
            return packages;

        }

        void          IBaseable.PushOnePackage(Package package)
        {
            var element = mDocument.CreateElement("package");
            foreach (var chain in package.Dictionary)
                (element.AppendChild(mDocument.CreateElement(chain.Key))).InnerText = chain.Value;
            mRoot.AppendChild(element);
            mDocument.Save(mPathToFile);
        }
        Package       IBaseable.PullOnePackage(Package package = null)
        {
            if (package == null)
                return FromNodeToPackage(mRoot.FirstChild); // возврат первого узла
            Package newPackage = null;
            foreach (XmlNode node in mRoot)
                if (CheckNodeAndPackage(node, package)) // проверка на соответствие узла требованиям
                    return FromNodeToPackage(node);
            return newPackage;
        }
    }
}
