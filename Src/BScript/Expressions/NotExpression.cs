namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualBasic.CompilerServices;

    public class NotExpression : IExpression
    {
        private IExpression expression;

        public NotExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            object value = this.expression.Evaluate(context);

            if (value == null || false.Equals(value))
                return true;

            return false;
        }
    }
}
