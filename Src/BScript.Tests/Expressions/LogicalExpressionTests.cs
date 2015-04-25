namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void EvaluateAndExpression()
        {
            Assert.AreEqual(false, EvaluateAnd(false, false));
            Assert.AreEqual(false, EvaluateAnd(null, null));
            Assert.AreEqual(false, EvaluateAnd(false, null));
            Assert.AreEqual(false, EvaluateAnd(null, false));

            Assert.AreEqual(false, EvaluateAnd(true, false));
            Assert.AreEqual(true, EvaluateAnd(true, true));
            Assert.AreEqual(false, EvaluateAnd(false, true));

            Assert.AreEqual(false, EvaluateAnd(true, null));
            Assert.AreEqual(false, EvaluateAnd(null, true));
        }

        [TestMethod]
        public void EvaluateNotExpression()
        {
            Assert.AreEqual(true, EvaluateNot(false));
            Assert.AreEqual(true, EvaluateNot(null));

            Assert.AreEqual(false, EvaluateNot(true));
            Assert.AreEqual(false, EvaluateNot(1));
            Assert.AreEqual(false, EvaluateNot("foo"));
        }

        private static object EvaluateOr(object left, object right)
        {
            return (new OrExpression(new ConstantExpression(left), new ConstantExpression(right))).Evaluate(null);
        }

        private static object EvaluateAnd(object left, object right)
        {
            return (new AndExpression(new ConstantExpression(left), new ConstantExpression(right))).Evaluate(null);
        }

        private static object EvaluateNot(object value)
        {
            return (new NotExpression(new ConstantExpression(value))).Evaluate(null);
        }
    }
}
