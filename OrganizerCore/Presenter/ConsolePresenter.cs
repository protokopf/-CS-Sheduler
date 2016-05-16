﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private ActionEventArgs ProceedCommand(ICommand command)
        {
            return command.Execute(lpModel);
        }

        public ConsolePresenter(ModelCore model, ConsoleView console)
        {
            lpModel = model;
            lpConsole = console;

            lpConsole.ConsoleCommands += ProceedCommand;
        }
        public void Proceed()
        {
            lpConsole.MainLoop();
        }

        
    }
}
