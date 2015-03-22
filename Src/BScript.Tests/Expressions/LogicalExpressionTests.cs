namespace BScript.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Expressions;

    [TestClass]
    public class LogicalExpressionTests
    {
        [TestMethod]
        public void EvaluateOrExpression()
        {
            Assert.AreEqual(false, EvaluateOr(false, false));
            Assert.AreEqual(false, EvaluateOr(null, null));
            Assert.AreEqual(false, EvaluateOr(false, null));
            Assert.AreEqual(false, EvaluateOr(null, false));

            Assert.AreEqual(true, EvaluateOr(true, false));
            Assert.AreEqual(true, EvaluateOr(true, true));
            Assert.AreEqual(true, EvaluateOr(false, true));

            Assert.AreEqual(true, EvaluateOr(true, null));
            Assert.AreEqual(true, EvaluateOr(null, true));
        }

        private static object EvaluateOr(object left, object right)
        {
            return (new OrExpression(new ConstantExpression(left), new ConstantExpression(right))).Evaluate(null);
        }
    }
}
