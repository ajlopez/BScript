namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NameExpression : BScript.Expressions.IExpression
    {
        private string name;

        public NameExpression(string name)
        {
            this.name = name;
        }

        public object Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            return context.GetValue(this.name);
        }
    }
}
