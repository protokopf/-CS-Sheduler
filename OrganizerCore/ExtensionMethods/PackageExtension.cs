using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizerCore.Model;

namespace OrganizerCore.ExtensionMethods
{
    public static class PackageExtension
    {
        static string FixedTime;
        static PackageExtension()
        {
            FixedTime = (DateTime.Now).ToString();
        }

        public static void CreateTestPackage(this Package package)
        {
            package.Dictionary.Add("ID", "666");
            package.Dictionary.Add("Name", "Kostya");
            package.Dictionary.Add("Description","KostyaDescription");
            package.Dictionary.Add("BeginTime", FixedTime);
            package.Dictionary.Add("EndTime", FixedTime);
        }
    }
}
