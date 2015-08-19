namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;
    using Microsoft.VisualBasic.CompilerServices;

    public class AndExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public AndExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(Context context)
        {
            object lvalue = this.left.Evaluate(context);

            if (Predicates.IsFalse(lvalue))
                return false;

            object rvalue = this.right.Evaluate(context);

            if (Predicates.IsFalse(rvalue))
                return false;

            return true;
        }
    }
}
