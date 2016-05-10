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
        public Package PackagedView
        {
            get { return mPackagedView; }
            set { mPackagedView = value; }
        }

        private readonly int mId;
        public int ID
        { 
            get { return mId; }
        }

        protected DateTime mBeginTime;
        public DateTime BeginTime
        {
            get { return mBeginTime; }
            set { mBeginTime = value; }
        }

        protected DateTime mEndTime;
        public DateTime EndTime
        {
            get { return mEndTime; }
            set { mEndTime = value; }
        }

        private string mName;
        public string  Name
        {
            get { return mName; }
            set { mName = value; }
        }

        private string mDescription;
        public string  Description
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
            return (currTime >= BeginTime)&&(currTime < EndTime);
        }

        public override string ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n{3}\n{4}\n",ID, Name, Description, BeginTime, EndTime);
        }
        public override bool Equals(object obj)
        {
            MyTask another = (MyTask)obj;
            return (this.PackagedView.Equals(another.PackagedView));
        }
    }
}
