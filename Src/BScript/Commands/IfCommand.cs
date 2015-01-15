namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class IfCommand : ICommand
    {
        private IExpression condition;
        private ICommand thencmd;

        public IfCommand(IExpression condition, ICommand thencmd)
        {
            this.condition = condition;
            this.thencmd = thencmd;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand ThenCommand { get { return this.thencmd; } }

        public void Execute(Context context)
        {
            if (IsFalse(this.condition.Evaluate(context)))
                return;

            this.thencmd.Execute(context);
        }

        private static bool IsFalse(object value)
        {
            return value == null || value.Equals(false);
        }
    }
}
