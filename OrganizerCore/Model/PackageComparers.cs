using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.Model
{
    public class FullEqualityComparer: IEqualityComparer<Package>
    {
        bool IEqualityComparer<Package>.Equals(Package p1, Package p2)
        {
            return p1.Equals(p2);
        }
        int IEqualityComparer<Package>.GetHashCode(Package p)
        {
            return base.GetHashCode();
        }
    }
}
