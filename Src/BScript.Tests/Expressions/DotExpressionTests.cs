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
    public class DotExpressionTests
    {
        [TestMethod]
        public void CreateDotExpression()
        {
            IExpression lexpr = new ConstantExpression(1);

            var expr = new DotExpression(lexpr, "Foo");

            Assert.AreEqual("Foo", expr.Name);
            Assert.AreSame(lexpr, expr.Expression);
        }

        [TestMethod]
        public void EvaluateDotExpression()
        {
            Context parent = new Context();
            Context context = new Context(parent);
            IExpression lexpr = new ConstantExpression(context);

            var expr = new DotExpression(lexpr, "Parent");
            var value = expr.Evaluate(null);

            Assert.IsNotNull(value);
            Assert.AreSame(parent, value);
        }
    }
}
