using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.WindowTypes;

namespace OrganizerCore.View.ExtraTypes
{
    interface IWindowDrawer
    {
        void CatchAllChild(BasicWindow parent);

        void AddGoal(IDrawable dr);
        void RemoveGoal(IDrawable dr);

        void InitialDraw();
        void Draw();
    }

    class WindowDrawer : IWindowDrawer
    {
        private List<IDrawable> mListOfGoals;

        public WindowDrawer()
        {
            mListOfGoals = new List<IDrawable>();
        }

        public void CatchAllChild(BasicWindow parent)
        {
            mListOfGoals.Add(parent);
            foreach (var child in parent.Childs)
                CatchAllChild(child);
        }

        public void AddGoal(IDrawable dr)
        {
            mListOfGoals.Add(dr);
        }
        public void RemoveGoal(IDrawable dr)
        {
            mListOfGoals.Remove(dr);
        }

        public void InitialDraw()
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
                if (!it.IsHidden())
                {
                    it.Clean();
                    it.Draw();
                }
            }
        }
    }
}
