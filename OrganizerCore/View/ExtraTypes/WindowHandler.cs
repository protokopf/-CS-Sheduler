using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.View.WindowTypes;

namespace OrganizerCore.View.ExtraTypes
{
    public interface IWindowHandler
    {
        BasicWindow this[string name] { get; }
        void CatchAllChild(BasicWindow parent);
    }

    public class WindowHandler : IWindowHandler
    {
        private Dictionary<string, BasicWindow> mStorage;
        private void RecursiveCatching(BasicWindow parent, ref StringBuilder path)
        {
            int startIndex = path.Length;
            path.Append('.' + parent.Name);
            mStorage.Add(path.ToString(), parent);  
            foreach (var child in parent.Childs)
                RecursiveCatching(child, ref path);
            path.Remove(startIndex, parent.Name.Length + 1);
        }

        public WindowHandler()
        {
            mStorage = new Dictionary<string, BasicWindow>();
        }

        public BasicWindow this[string name]
        {
            get 
            {
                return mStorage[name];
            }
        }
        public void CatchAllChild(BasicWindow parent)
        {
            StringBuilder pathToWindow = new StringBuilder();
            pathToWindow.Append(parent.Name);
            mStorage.Add(pathToWindow.ToString(), parent);
            foreach(var child in parent.Childs)
                RecursiveCatching(child, ref pathToWindow);
        }
    }
}
