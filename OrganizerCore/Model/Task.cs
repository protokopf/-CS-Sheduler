using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OrganizerCore.Model
{


    public class MyTask
    {
        private static int taskId = 0;

        private static List<PropertyInfo> mProperties;
        public List<PropertyInfo> GetMyProperties()
        {
            return mProperties;
        }

        static MyTask()
        {
            mProperties = ((typeof(MyTask)).GetProperties()).ToList();
        }

        private Package mPackagedView;
        public void  SetPackageView(Package p)
        {
            mPackagedView = p;
        }
        public Package  GetPackageView()
        {
            return mPackagedView;
        }

        private readonly int mId;
        public int ID
        { 
            get { return mId; }
        }

        protected   DateTime mDate;
        public      DateTime Date
        {
            get { return mDate; }
            set { mDate = value; }
        }

        private     string mName;
        public      string  Name
        {
            get { return mName; }
            set { mName = value; }
        }

        private     string mDescription;
        public      string  Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        public MyTask ()
        {
            mId = taskId++;
        }
        public bool IsTime()
        {
            var currTime = DateTime.Now;
            return currTime >= Date;
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} ({2})", Name, Description, Date.ToString());
        }
        public override bool Equals(object obj)
        {
            MyTask another = (MyTask)obj;
            return (this.GetPackageView().Equals(another.GetPackageView()));
        }
    }
}
