using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrganizerCore.Model
{
    public class ModelCore
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

        private AbstractConvertor mStorageConvertor;
        private IEqualityComparer<Package> mPackageEquater;

        public ModelCore(IModelBaseCommunicator storageRef, IEqualityComparer<Package> packageEquater)
        {
            mPackageEquater = packageEquater;
            mCoreStorage = storageRef;
            mTaskList = new List<MyTask>();
            mStorageConvertor = new Convertor();
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
                task.PackagedView = package;
                mTaskList.Add(task);
            }
        }

        public MyTask CheckIfTaskExists(Package package)
        {
            foreach (var task in mTaskList)
                if (mPackageEquater.Equals(package, task.PackagedView))
                    return task;
            return null;
        }

        public void    AddTask(Package package)
        {
            if (CheckIfTaskExists(package) == null)
            {
                MyTask newtask = null;
                newtask = mStorageConvertor.FromPackageToMyTask(package);
                newtask.PackagedView = package;
                mTaskList.Add(newtask);
            }
        }
        public void    DeleteTask(Package package)
        {
            MyTask deletedTask = CheckIfTaskExists(package);
            if(deletedTask != null)
                mTaskList.Remove(deletedTask);
        }
        public Package GetTask(Package package) // в случае отсутствия пакета, возвращает null
        {
            return (CheckIfTaskExists(package)).PackagedView;
        }
        
    }
}
