namespace BScript.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Compiler;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseNull()
        {
            Parser parser = new Parser(null);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseEmptyText()
        {
            Parser parser = new Parser(string.Empty);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseWhiteSpaceText()
        {
            Parser parser = new Parser("   ");

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo\"");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));
            Assert.AreEqual("foo", ((ConstantExpression)result).Value);
        }

        [TestMethod]
        public void ParseName()
        {
            Parser parser = new Parser("foo");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NameExpression));
            Assert.AreEqual("foo", ((NameExpression)result).Name);
        }

        [TestMethod]
        public void ParseAssignment()
        {
            Parser parser = new Parser("foo=1");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AssignExpression));

            var aexpr = (AssignExpression)result;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);
        }

        [TestMethod]
        public void ParseAddIntegers()
        {
            Parser parser = new Parser("1+2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Add, 1, 2); 
        }

        [TestMethod]
        public void ParseAddThreeIntegers()
        {
            Parser parser = new Parser("1+2+3");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BinaryOperatorExpression));

            var bexpr = (BinaryOperatorExpression)result;

            Assert.AreEqual(BinaryOperator.Add, bexpr.Operator);
            IsBinaryOperation(bexpr.LeftExpression, BinaryOperator.Add, 1, 2);
            Assert.IsInstanceOfType(bexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(3, ((ConstantExpression)bexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractIntegers()
        {
            Parser parser = new Parser("1-2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Subtract, 1, 2); 
        }

        [TestMethod]
        public void ParseMultiplyIntegers()
        {
            Parser parser = new Parser("2*3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Multiply, 2, 3);
        }

        [TestMethod]
        public void ParseDivideIntegers()
        {
            Parser parser = new Parser("2/3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Divide, 2, 3);
        }

        [TestMethod]
        public void ParseExpressionCommand()
        {
            Parser parser = new Parser("foo=1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseExpressionCommandSkippingNewLines()
        {
            Parser parser = new Parser("\n\r\n\rfoo=1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseTwoExpressionCommands()
        {
            Parser parser = new Parser("foo=1\nbar=2");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("bar", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseWhenNoEndOfCommand()
        {
            Parser parser = new Parser("foo=1 a\nbar=2");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Unexpected 'a'", ex.Message);
            }
        }

        private static void IsBinaryOperation(IExpression expr, BinaryOperator oper, int left, int right)
        {
            Assert.IsInstanceOfType(expr, typeof(BinaryOperatorExpression));
            var bexpr = (BinaryOperatorExpression)expr;

            Assert.AreEqual(oper, bexpr.Operator);
            Assert.IsInstanceOfType(bexpr.LeftExpression, typeof(ConstantExpression));
            Assert.AreEqual(left, ((ConstantExpression)bexpr.LeftExpression).Value);
            Assert.IsInstanceOfType(bexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(right, ((ConstantExpression)bexpr.RightExpression).Value);
        }
    }
}
