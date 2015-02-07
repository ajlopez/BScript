namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class CompositeCommand : ICommand
    {
        private IList<ICommand> commands;

        public CompositeCommand(IList<ICommand> commands)
        {
            this.commands = commands;
        }

        public IList<ICommand> Commands { get { return this.commands; } }

        public void Execute(Context context)
        {
            foreach (var cmd in this.commands)
                cmd.Execute(context);
        }
    }
}
