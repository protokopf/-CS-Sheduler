using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.WindowTypes;

namespace OrganizerCore.View.ExtraTypes
{
    class Drawer
    {
        private List<IDrawable> mListOfGoals;
        
        public Drawer()
        {
            mListOfGoals = new List<IDrawable>();
        }


        public void AddGoal(IDrawable dr)
        {
            mListOfGoals.Add(dr);
        }
        public void RemoveGoal(IDrawable dr)
        {
            mListOfGoals.Remove(dr);
        }

        public void InitialDrawing()
        {
            foreach (var goal in mListOfGoals)
                if(!goal.IsHidden())
                    goal.Draw();
        }
        public void Draw()
        {
            var changedItems = (from chI in mListOfGoals where (chI.IsChanged() == true) select chI);
            foreach(var it in changedItems)
            {
                it.Clean();
                if(!it.IsHidden())
                    it.Draw();
            }
        }
    }
}
