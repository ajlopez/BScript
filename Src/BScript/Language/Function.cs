namespace BScript.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;

    public class Function
    {
        private IList<string> argnames;
        private ICommand body;

        public Function(IList<string> argnames, ICommand body)
        {
            this.argnames = argnames;
            this.body = body;
        }

        public IList<string> ArgumentNames { get { return this.argnames; } }

        public ICommand Body { get { return this.body; } }

        public object Evaluate(Context parent, IList<object> values)
        {
            Context context = new Context(parent);

            for (int k = 0; k < argnames.Count; k++)
                context.SetValue(argnames[k], values[k]);

            this.body.Execute(context);

            if (context.HasReturn)
                return context.ReturnValue;
            else
                return null;
        }
    }
}

