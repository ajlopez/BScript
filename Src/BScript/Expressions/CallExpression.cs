namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;

    public class CallExpression : BScript.Expressions.IExpression
    {
        private string name;
        private IList<IExpression> argexprs;

        public CallExpression(string name, IList<IExpression> argexprs)
        {
            this.name = name;
            this.argexprs = argexprs;
        }

        public string Name { get { return this.name; } }

        public IList<IExpression> ArgumentExpressions { get { return this.argexprs; } }

        public object Evaluate(Context context)
        {
            Function func = (Function)context.GetValue(this.name);
            IList<object> args = new List<object>();

            foreach (var expr in this.argexprs)
                args.Add(expr.Evaluate(context));

            return func.Evaluate(context, args);
        }
    }
}
