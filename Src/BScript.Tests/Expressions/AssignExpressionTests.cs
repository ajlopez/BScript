namespace BScript.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Expressions;

    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void AssignInteger()
        {
            Context context = new Context();
            IExpression expr = new ConstantExpression(42);
            AssignExpression aexpr = new AssignExpression("a", expr);

            Assert.AreEqual("a", aexpr.Name);
            Assert.AreSame(expr, aexpr.Expression);

            var result = aexpr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
            Assert.AreEqual(42, context.GetValue("a"));
        }
    }
}
