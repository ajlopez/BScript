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
        public void OrFalseFalse()
        {
            var lexpr = new ConstantExpression(false);
            var rexpr = new ConstantExpression(false);
            var expr = new OrExpression(lexpr, rexpr);

            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.AreSame(rexpr, expr.RightExpression);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void OrNullFalse()
        {
            var lexpr = new ConstantExpression(null);
            var rexpr = new ConstantExpression(false);
            var expr = new OrExpression(lexpr, rexpr);

            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.AreSame(rexpr, expr.RightExpression);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void OrFalseNull()
        {
            var lexpr = new ConstantExpression(false);
            var rexpr = new ConstantExpression(null);
            var expr = new OrExpression(lexpr, rexpr);

            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.AreSame(rexpr, expr.RightExpression);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void OrFalseTrue()
        {
            var lexpr = new ConstantExpression(false);
            var rexpr = new ConstantExpression(true);
            var expr = new OrExpression(lexpr, rexpr);

            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.AreSame(rexpr, expr.RightExpression);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void OrTrueFalse()
        {
            var lexpr = new ConstantExpression(true);
            var rexpr = new ConstantExpression(false);
            var expr = new OrExpression(lexpr, rexpr);

            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.AreSame(rexpr, expr.RightExpression);

            Assert.AreEqual(true, expr.Evaluate(null));
        }
    }
}
