namespace BScript.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;

    public class ForCommand : ICommand
    {
        private string name;
        private IExpression fromexpr;
        private IExpression toexpr;
        private IExpression stepexpr;
        private ICommand body;

        public ForCommand(string name, IExpression fromexpr, IExpression toexpr, ICommand body)
            : this(name, fromexpr, toexpr, null, body)
        {
        }

        public ForCommand(string name, IExpression fromexpr, IExpression toexpr, IExpression stepexpr, ICommand body)
        {
            this.name = name;
            this.fromexpr = fromexpr;
            this.toexpr = toexpr;
            this.stepexpr = stepexpr;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IExpression FromExpression { get { return this.fromexpr; } }

        public IExpression ToExpression { get { return this.toexpr; } }

        public IExpression StepExpression { get { return this.stepexpr; } }

        public ICommand Body { get { return this.body; } }

        public void Execute(Context context)
        {
            object from = this.fromexpr.Evaluate(context);
            object to = this.toexpr.Evaluate(context);
            context.SetValue(this.name, from);
            AssignExpression increxpr;
            
            if (this.stepexpr == null) 
                increxpr = new AssignExpression(this.name, new BinaryOperatorExpression(BinaryOperator.Add, new NameExpression(this.name), new ConstantExpression(1)));
            else
                increxpr = new AssignExpression(this.name, new BinaryOperatorExpression(BinaryOperator.Add, new NameExpression(this.name), this.stepexpr));

            while (((IComparable)context.GetValue(this.name)).CompareTo(to) != 1)
            {
                this.body.Execute(context);
                increxpr.Evaluate(context);
            }
        }
    }
}
