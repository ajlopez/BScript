namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class IncludeCommand : ICommand
    {
        IExpression expr;

        public IncludeCommand(IExpression expr)
        {
            this.expr = expr;
        }

        public IExpression Expression { get { return this.expr; } }

        public void Execute(Context context)
        {
            string filename = (string)this.expr.Evaluate(context);
            Machine.ExecuteFile(filename, context);
        }
    }
}
