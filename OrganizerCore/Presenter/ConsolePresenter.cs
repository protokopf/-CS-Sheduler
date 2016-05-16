using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using OrganizerCore.View;
using OrganizerCore.View.ExtraTypes;

using OrganizerCore.Model;

using OrganizerCore.Presenter.Commands;

namespace OrganizerCore.Presenter
{
    class ConsolePresenter
    {
        private ConsoleView lpConsole;
        private ModelCore   lpModel;

        private Timer mTimer;

        private ActionEventArgs ProceedCommand(ICommand command)
        {
            return command.Execute(lpModel);
        }
        private void CheckPresenceMessages(object source, ElapsedEventArgs e)
        {
            string msg = lpModel.CheckEventAdvent();

            if (msg != null)
                lpConsole.AddMessageInQueue(msg);
        }

        public ConsolePresenter(ModelCore model, ConsoleView console)
        {
            mTimer = new Timer(1000);

            lpModel = model;
            lpConsole = console;

            lpConsole.ConsoleCommands += ProceedCommand;
            mTimer.Elapsed += CheckPresenceMessages;
        }

        public void Proceed()
        {
            mTimer.Start();
            lpConsole.MainLoop();
        }

        
    }
}
