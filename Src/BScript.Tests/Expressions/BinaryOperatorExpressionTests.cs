namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BinaryOperatorExpressionTests
    {
        [TestMethod]
        public void CreateExpression()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(1);
            var expr = new BinaryOperatorExpression(BinaryOperator.Add, lexpr, rexpr);

            Assert.AreEqual(BinaryOperator.Add, expr.Operator);
            Assert.IsNotNull(expr.LeftExpression);
            Assert.AreSame(lexpr, expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.AreSame(rexpr, expr.RightExpression);
        }

        [TestMethod]
        public void AddIntegers()
        {
            var lexpr = new ConstantExpression(40);
            var rexpr = new ConstantExpression(2);
            var expr = new BinaryOperatorExpression(BinaryOperator.Add, lexpr, rexpr);

            Assert.AreEqual(42, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractIntegers()
        {
            var lexpr = new ConstantExpression(44);
            var rexpr = new ConstantExpression(2);
            var expr = new BinaryOperatorExpression(BinaryOperator.Subtract, lexpr, rexpr);

            Assert.AreEqual(42, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyIntegers()
        {
            var lexpr = new ConstantExpression(21);
            var rexpr = new ConstantExpression(2);
            var expr = new BinaryOperatorExpression(BinaryOperator.Multiply, lexpr, rexpr);

            Assert.AreEqual(42, expr.Evaluate(null));
        }

        [TestMethod]
        public void DivideIntegers()
        {
            var lexpr = new ConstantExpression(84);
            var rexpr = new ConstantExpression(2);
            var expr = new BinaryOperatorExpression(BinaryOperator.Divide, lexpr, rexpr);

            Assert.AreEqual(42.0, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualInteger()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.Equal, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualIntegers()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(1);
            var expr = new BinaryOperatorExpression(BinaryOperator.Equal, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualNulls()
        {
            var lexpr = new ConstantExpression(null);
            var rexpr = new ConstantExpression(null);
            var expr = new BinaryOperatorExpression(BinaryOperator.Equal, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void EqualNullNotNull()
        {
            var lexpr = new ConstantExpression(null);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.Equal, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NotEqualInteger()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.NotEqual, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NotEqualIntegers()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(1);
            var expr = new BinaryOperatorExpression(BinaryOperator.NotEqual, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void NotEqualNulls()
        {
            var lexpr = new ConstantExpression(null);
            var rexpr = new ConstantExpression(null);
            var expr = new BinaryOperatorExpression(BinaryOperator.NotEqual, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NotEqualNullNotNull()
        {
            var lexpr = new ConstantExpression(null);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.NotEqual, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void LessInteger()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.Less, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void LessIntegers()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(43);
            var expr = new BinaryOperatorExpression(BinaryOperator.Less, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void GreaterInteger()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(42);
            var expr = new BinaryOperatorExpression(BinaryOperator.Greater, lexpr, rexpr);

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void GreaterIntegers()
        {
            var lexpr = new ConstantExpression(42);
            var rexpr = new ConstantExpression(41);
            var expr = new BinaryOperatorExpression(BinaryOperator.Greater, lexpr, rexpr);

            Assert.AreEqual(true, expr.Evaluate(null));
        }
    }
}
