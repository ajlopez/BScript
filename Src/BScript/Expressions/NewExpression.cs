namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;

    public class NewExpression : IExpression
    {
        private IExpression expression;
        private IList<IExpression> argexprs;

        public NewExpression(IExpression expression, IList<IExpression> argexprs)
        {
            this.expression = expression;
            this.argexprs = argexprs;
        }

        public IExpression Expression { get { return this.expression; } }

        public IList<IExpression> ArgumentExpressions { get { return this.argexprs; } }

        public object Evaluate(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
