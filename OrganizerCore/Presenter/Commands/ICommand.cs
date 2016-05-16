using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OrganizerCore.Model;

namespace OrganizerCore.Presenter.Commands
{
    interface ICommand
    {
        void execute(ModelCore model);
    }

    public class UpdateListCommand : ICommand
    {
        private List<string> mStrings;
        public UpdateListCommand(List<string> info)
        {
            mStrings = info;
        }

        void execute(ModelCore model)
        {
            mStrings = model.GetTaskList();
        }
    }
}
