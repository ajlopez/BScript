namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class ReturnCommand : ICommand
    {
        private IExpression expression;

        public ReturnCommand(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public void Execute(Context context)
        {
            context.HasReturn = true;

            if (this.expression != null)
                context.ReturnValue = this.expression.Evaluate(context);
        }
    }
}
