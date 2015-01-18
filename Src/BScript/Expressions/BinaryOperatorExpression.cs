﻿namespace BScript.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualBasic.CompilerServices;

    public class BinaryOperatorExpression : IExpression
    {
        private Func<object, object, object> operfn;
        private BinaryOperator oper;
        private IExpression left;
        private IExpression right;

        public BinaryOperatorExpression(BinaryOperator oper, IExpression left, IExpression right)
        {
            this.oper = oper;
            this.left = left;
            this.right = right;

            switch (oper)
            {
                case BinaryOperator.Add:
                    this.operfn = Operators.AddObject;
                    break;
                case BinaryOperator.Subtract:
                    this.operfn = Operators.SubtractObject;
                    break;
                case BinaryOperator.Multiply:
                    this.operfn = Operators.MultiplyObject;
                    break;
                case BinaryOperator.Divide:
                    this.operfn = Operators.DivideObject;
                    break;
            }
        }

        public BinaryOperator Operator { get { return this.oper; } }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(Context context)
        {
            return this.operfn(this.left.Evaluate(context), this.right.Evaluate(context));
        }
    }
}
