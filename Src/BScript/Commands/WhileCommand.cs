namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class WhileCommand : ICommand
    {
        private IExpression condition;
        private ICommand cmd;

        public WhileCommand(IExpression condition, ICommand cmd)
        {
            this.condition = condition;
            this.cmd = cmd;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand Command { get { return this.cmd; } }

        public void Execute(Context context)
        {
            while (!IsFalse(this.condition.Evaluate(context)))
                this.cmd.Execute(context);
        }

        private static bool IsFalse(object value)
        {
            return value == null || value.Equals(false);
        }
    }
}
