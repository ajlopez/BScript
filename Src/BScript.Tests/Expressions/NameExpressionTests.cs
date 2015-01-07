namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateNameExpression()
        {
            var ctx = new Context();
            ctx.SetValue("Answer", 42);

            var expr = new NameExpression("Answer");

            Assert.AreEqual("Answer", expr.Name);
            Assert.AreEqual(42, expr.Evaluate(ctx));
        }

        [TestMethod]
        public void CreateAndEvaluateUnknownNameExpression()
        {
            var ctx = new Context();

            var expr = new NameExpression("Unknown");

            Assert.AreEqual("Unknown", expr.Name);
            Assert.IsNull(expr.Evaluate(ctx));
        }
    }
}
