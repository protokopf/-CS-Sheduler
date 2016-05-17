using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.Model;
using OrganizerCore.View.ExtraTypes;

namespace OrganizerCore.Presenter.Commands
{
    interface ICommand
    {
        ActionEventArgs Execute(ModelCore model);
    }

    public class UpdateListCommand : ICommand
    {
        private Dictionary<int, string> mStrings;

        public ActionEventArgs Execute(ModelCore model)
        {
            mStrings = model.GetTaskList();
            ActionEventArgs e = new ActionEventArgs();
            foreach (var pair in mStrings)
                e.Storage.Add(pair.Key.ToString(), pair.Value);
            return e;
        }
    }

    public class AddEventCommand : ICommand
    {
        ActionEventArgs arguments;

        public AddEventCommand(ActionEventArgs e)
        {
            arguments = e;
        }

        public ActionEventArgs Execute(ModelCore model)
        {
            model.AddTask(arguments.Storage);
            return null;
        }
    }
    public class DeleteEventCommand : ICommand
    {
        private int index;
        public DeleteEventCommand(int i)
        {
            index = i;
        }

        public ActionEventArgs Execute(ModelCore model)
        {
            model.DeleteTask(index);
            return null;
        }
    }
}
