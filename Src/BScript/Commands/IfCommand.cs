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
        private ICommand elsecmd;

        public IfCommand(IExpression condition, ICommand thencmd)
            : this(condition, thencmd, null)
        {
        }

        public IfCommand(IExpression condition, ICommand thencmd, ICommand elsecmd)
        {
            this.condition = condition;
            this.thencmd = thencmd;
            this.elsecmd = elsecmd;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand ThenCommand { get { return this.thencmd; } }

        public ICommand ElseCommand { get { return this.elsecmd; } }

        public void Execute(Context context)
        {
            if (IsFalse(this.condition.Evaluate(context)))
            {
                if (this.elsecmd != null)
                    this.elsecmd.Execute(context);
                return;
            }

            this.thencmd.Execute(context);
        }

        private static bool IsFalse(object value)
        {
            return value == null || value.Equals(false);
        }
    }
}
