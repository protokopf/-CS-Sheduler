using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace OrganizerCore.Model
{
    public interface AbstractConvertor
    {
       Package FromMyTaskToPackage(MyTask MyTask);
       MyTask  FromPackageToMyTask(Package package);
    }

    public class Convertor : AbstractConvertor
    {
        MyTask AbstractConvertor.FromPackageToMyTask(Package package)
        {
            MyTask task = new MyTask();
            Type taskType = task.GetType();
            foreach(var chain in package.Dictionary)
            {
                PropertyInfo prop = taskType.GetProperty(chain.Key);
                if (prop.CanWrite)
                {
                    MethodInfo meth = prop.PropertyType.GetMethod("Parse", new Type[] { typeof(string) }, null);
                    var value = (meth != null) ? meth.Invoke(null, new Object[] { chain.Value }) : chain.Value;
                    prop.SetValue(task, value);
                }
            }
            return task;
        }
        Package AbstractConvertor.FromMyTaskToPackage(MyTask task)
        {
            Package package = new Package();
            foreach(var prop in task.GetMyProperties())
                package.Dictionary.Add(prop.Name,prop.GetValue(task).ToString());
            return package;
        }
    }
}
