using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.Model
{

    public class Package
    {
        private Dictionary<string, string> mNamesAndValues;
        public  Dictionary<string, string> Dictionary
        {
            get { return mNamesAndValues; }
        }
        public  Package()
        {
            mNamesAndValues = new Dictionary<string, string>();
        }

        public override bool Equals(object obj)
        {
            Package anotherPackage = (Package)obj;
            bool equality = (this.Dictionary.Count == anotherPackage.Dictionary.Count);
            if (!equality)
                return false;
            foreach(var chain in this.Dictionary)
                if (chain.Value != anotherPackage.Dictionary[chain.Key])
                    return false;
            return equality;
        }
    }
}
