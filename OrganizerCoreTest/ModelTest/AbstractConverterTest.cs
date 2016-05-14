using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using NUnit;

using OrganizerCore.Model;

namespace OrganizerCoreTest.ModelTest
{
    [TestFixture]
    static class AbstractConverterTest
    {
        [Test]
        static public void Convert_Task_Package()
        {
            AbstractConvertor convertor = new Convertor();
            MyTask task = new MyTask()
            {
                Name = "Hello",
                Description = "Desc",
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now
            };

            Package package = convertor.FromMyTaskToPackage(task);

            Assert.AreEqual(task.PackagedView, package);
        }
        [Test]
        static public void Convert_Package_Task()
        {
            AbstractConvertor convertor = new Convertor();
            MyTask task = new MyTask()
            {
                Name = "Hello",
                Description = "Desc",
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now
            };

            Package package = convertor.FromMyTaskToPackage(task);
            MyTask task2 = convertor.FromPackageToMyTask(package);

            Assert.AreEqual(task.PackagedView, task2.PackagedView);
        }
    }
}
