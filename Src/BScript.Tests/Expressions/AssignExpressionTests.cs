namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
