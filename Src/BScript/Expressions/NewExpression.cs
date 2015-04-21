namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Language;
    using BScript.Utilities;

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
            Type type;
                
            if (this.expression is DotExpression)
                type = TypeUtilities.GetType(((DotExpression)this.expression).FullName);
            else
                type = (Type)this.expression.Evaluate(context);

            IList<object> args = new List<object>();

            foreach (var argexpr in this.argexprs)
                args.Add(argexpr.Evaluate(context));

            return Activator.CreateInstance(type, args.ToArray());
        }
    }
}
