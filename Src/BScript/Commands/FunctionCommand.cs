namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using BScript.Language;

    public class FunctionCommand : ICommand
    {
        private string name;
        private IList<string> argnames;
        private ICommand body;

        public FunctionCommand(string name, IList<string> argnames, ICommand body)
        {
            this.name = name;
            this.argnames = argnames;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<string> ArgumentNames { get { return this.argnames; } }

        public ICommand Body { get { return this.body; } }

        public void Execute(Context context)
        {
            Function func = new Function(this.argnames, this.body);
            context.SetValue(this.name, func);
        }
    }
}
