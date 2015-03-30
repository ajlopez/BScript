namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Expressions;
    using BScript.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NewExpressionTests
    {
        [TestMethod]
        public void CreateNewExpression()
        {
            IExpression texpr = new ConstantExpression(typeof(Context));
            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(1), new ConstantExpression(2) };

            var expr = new NewExpression(texpr, exprs);

            Assert.AreSame(texpr, expr.Expression);
            Assert.AreSame(exprs, expr.ArgumentExpressions);
        }

        [TestMethod]
        public void EvaluateNewExpression()
        {
            Context parent = new Context();
            IExpression texpr = new ConstantExpression(typeof(Context));
            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(parent) };

            var expr = new NewExpression(texpr, exprs);

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Context));

            var context = (Context)result;

            Assert.AreSame(parent, context.Parent);
        }
    }
}
