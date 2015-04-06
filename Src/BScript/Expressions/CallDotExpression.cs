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
            var value = this.expression.Expression.Evaluate(context);
            var type = value.GetType();
            IList<object> args = new List<object>();

            foreach (var expr in this.argexprs)
                args.Add(expr.Evaluate(context));

            return type.InvokeMember(this.expression.Name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod, null, value, args.ToArray());
        }
    }
}
