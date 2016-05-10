using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit;
using NUnit.Framework;

using OrganizerCore.Model;
using OrganizerCore.ExtensionMethods;

namespace OrganizerCoreTest.ModelTest
{
    [TestFixture]
    class ModelCoreTest
    {
        [Test]
        public static void Add_New_Task()
        {
            IEqualityComparer<Package> equater = new FullEqualityComparer();
            ModelCore model = new ModelCore(null, equater);
            Package pack = new Package();

            pack.CreateTestPackage();
            model.AddTask(pack);
            Package gettedPack = model.GetTask(pack);

            Assert.AreEqual(gettedPack, pack);
        }
        [Test]
        public static void Delete_Task()
        {
            IEqualityComparer<Package> equater = new FullEqualityComparer();
            ModelCore model = new ModelCore(null, equater);
            Package package = new Package();

            package.CreateTestPackage();
            model.AddTask(package);
            model.DeleteTask(package);

            Assert.IsNull(model.CheckIfTaskExists(package));
        }
    }
}
