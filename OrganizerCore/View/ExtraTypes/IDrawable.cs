using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerCore.View.ExtraTypes
{
    interface IDrawable
    {
        void Draw();
        void Clean();
        bool IsChanged();
    }
}
