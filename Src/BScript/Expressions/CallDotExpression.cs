namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;

    public class CallDotExpression : IExpression
    {
        private DotExpression expression;
        private IList<IExpression> argexprs;

        public CallDotExpression(DotExpression expression, IList<IExpression> argexprs)
        {
            this.expression = expression;
            this.argexprs = argexprs;
        }

        public DotExpression Expression { get { return this.expression; } }

        public IList<IExpression> ArgumentExpressions { get { return this.argexprs; } }

        public object Evaluate(Context context)
        {
            IList<object> args = new List<object>();

            foreach (var expr in this.argexprs)
                args.Add(expr.Evaluate(context));

            throw new NotImplementedException();
        }
    }
}
