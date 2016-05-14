using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.Model;
using OrganizerCore.PackageBases;

using AbstractBase = OrganizerCore.Model.ModelCore.IModelBaseCommunicator;

namespace OrganizerCoreTest.PackageBaseTest
{
    static class PackageBasesTest
    {
        public static void Push_List_Of_Packages()
        {
            MyTask t1 = new MyTask {Name = "Kostya", Description = "Desc1", BeginTime = DateTime.Now, EndTime = DateTime.Now};
            MyTask t2 = new MyTask {Name = "Pasha", Description = "Desc2", BeginTime = DateTime.Now, EndTime = DateTime.Now};
            AbstractConvertor convertor = new Convertor();
            List<Package> pList = new List<Package>
            {
                convertor.FromMyTaskToPackage(t1),
                convertor.FromMyTaskToPackage(t2)
            };
            ModelCore.IModelBaseCommunicator dataBase = new XMLBase();
            dataBase.PushPackagesList(pList);
        }
        public static void Pull_List_Of_Packages()
        {
            ModelCore.IModelBaseCommunicator dataBase = new XMLBase();
            AbstractConvertor convertor = new Convertor();
            var pList = dataBase.PullPackagesList();
            List<MyTask> tasks = new List<MyTask>();
            foreach (var package in pList)
                tasks.Add(convertor.FromPackageToMyTask(package));
            foreach(var task in tasks)
                Console.WriteLine(task.ToString());
        }

        public static void Pull_One_Package()
        {
            AbstractBase xmlBase = new XMLBase();
            xmlBase.OpenBase();

            Package searchedPackage = new Package();
            string name = "Kos";
            searchedPackage.Dictionary.Add("Name", name);

            Package pulledPackage = xmlBase.PullOnePackage(searchedPackage);

            Console.WriteLine((pulledPackage.Dictionary["Name"]).Equals(name));
        }
        public static void Push_One_Package()
        {
            Package pack = new Package();
            pack.Dictionary.Add("Name", "Blabla");

            AbstractBase xmlBase = new XMLBase();
            xmlBase.OpenBase();

            xmlBase.PushOnePackage(pack);
        }
    }
}
