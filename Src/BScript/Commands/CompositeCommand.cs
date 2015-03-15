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
            context.HasReturn = false;

            foreach (var cmd in this.commands.Where(cmd => cmd is FunctionCommand))
                cmd.Execute(context);

            foreach (var cmd in this.commands.Where(cmd => !(cmd is FunctionCommand)))
            {
                cmd.Execute(context);

                if (context.HasReturn)
                    return;
            }
        }
    }
}
