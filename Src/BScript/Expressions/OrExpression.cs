namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualBasic.CompilerServices;

    public class OrExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public OrExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(Context context)
        {
            object lvalue = this.left.Evaluate(context);

            if (lvalue != null && !false.Equals(lvalue))
                return true;

            object rvalue = this.right.Evaluate(context);

            if (rvalue != null && !false.Equals(rvalue))
                return true;

            return false;
        }
    }
}
