using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrganizerCore.Model
{
    public class ModelCore : IDisposable
    {
        public interface IModelBaseCommunicator
        {
            void          PushPackagesList(List<Package> pList);
            List<Package> PullPackagesList();
            void          PushOnePackage(Package package);
            Package       PullOnePackage(Package package = null);
            void          OpenBase();
            void          CloseBase();
        }
        private IModelBaseCommunicator mCoreStorage;

        private List<MyTask> mTaskList;

        private StringBuilder mMessagesBuilder;

        private AbstractConvertor mStorageConvertor;
        private IEqualityComparer<Package> mPackageEquater;

        public ModelCore(IModelBaseCommunicator storageRef, IEqualityComparer<Package> packageEquater)
        {
            mPackageEquater = packageEquater;
            mCoreStorage = storageRef;

            mTaskList = new List<MyTask>();
            mStorageConvertor = new Convertor();
            mMessagesBuilder = new StringBuilder();

            mCoreStorage.OpenBase();

            LoadAllTasks();
        }

        public void SaveAllTasks()
        {
            var pList = new List<Package>();
            foreach (var task in mTaskList)
                pList.Add(mStorageConvertor.FromMyTaskToPackage(task));
            mCoreStorage.PushPackagesList(pList);
        }
        public void LoadAllTasks()
        {
            var pList = mCoreStorage.PullPackagesList();
            foreach (var package in pList)
            {
                MyTask task = mStorageConvertor.FromPackageToMyTask(package);
                task.SetPackageView(package);
                mTaskList.Add(task);
            }
        }

        public MyTask CheckIfTaskExists(Package package)
        {
            foreach (var task in mTaskList)
                if (mPackageEquater.Equals(package, task.GetPackageView()))
                    return task;
            return null;
        }

        public Dictionary<int,string> GetTaskList()
        {
            Dictionary<int, string> tasks = new Dictionary<int, string>();
            foreach (var task in mTaskList)
                tasks.Add(task.ID,task.ToString());
            return tasks;
        }
        public string CheckEventAdvent()
        {
            mMessagesBuilder.Clear();
            foreach(var ev in mTaskList)
            {
                if(ev.IsTime())
                {
                    mMessagesBuilder.Append(ev.Name).Append(" - ").Append(ev.Description);
                    mTaskList.Remove(ev);
                    return mMessagesBuilder.ToString();
                }
            }
            return null;
        }

        public void    AddTask(Package package)
        {
            if (CheckIfTaskExists(package) == null)
            {
                MyTask newtask = null;
                newtask = mStorageConvertor.FromPackageToMyTask(package);
                newtask.SetPackageView(package);
                mTaskList.Add(newtask);
            }
        }

        public void    DeleteTask(Package package)
        {
            MyTask deletedTask = CheckIfTaskExists(package);
            if(deletedTask != null)
                mTaskList.Remove(deletedTask);
        }
        public void    DeleteTask(int index)
        {
            mTaskList.RemoveRange(index, 1);
        }

        public Package GetTask(Package package) // в случае отсутствия пакета, возвращает null
        {
            return (CheckIfTaskExists(package)).GetPackageView();
        }
        
        public void Dispose()
        {
            SaveAllTasks();
        }
    }
}
