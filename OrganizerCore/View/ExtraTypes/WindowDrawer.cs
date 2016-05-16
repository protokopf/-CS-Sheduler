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
        int Capacity { get; }

        void CatchAllChild(BasicWindow parent);

        void CompleteParentWindow(BasicWindow parent);
        void CutParentWindow(BasicWindow parent);

        void AddGoal(IDrawable dr);
        void RemoveGoal(IDrawable dr);

        void InitialDraw();
        void Draw();
    }

    class WindowDrawer : IWindowDrawer
    {
        private List<IDrawable> mListOfGoals;

        
        public int Capacity { get { return mListOfGoals.Count; } }

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

        public void CompleteParentWindow(BasicWindow parent)
        {
            foreach (var child in parent.Childs)
                    mListOfGoals.Add(child);
        }
        public void CutParentWindow(BasicWindow parent)
        {
            foreach (var child in parent.Childs)
                mListOfGoals.Remove(child);
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
                if (!goal.IsHidden())
                    goal.Draw();
        }
        public void Draw()
        {
            var changedItems = (from chI in mListOfGoals where (chI.IsChanged() == true) select chI);
            foreach (var it in changedItems)
            {
                it.Clean();
                if (!it.IsHidden())
                    it.Draw();
            }
        }
    }
}
