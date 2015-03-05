namespace BScript.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Compiler;

    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void EvaluateNumber()
        {
            var result = EvaluateExpression("42");

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void EvaluateString()
        {
            var result = EvaluateExpression("\"foo\"");

            Assert.IsNotNull(result);
            Assert.AreEqual("foo", result);
        }

        [TestMethod]
        public void EvaluateVariable()
        {
            var context = new Context();
            context.SetValue("a", 42);

            var result = EvaluateExpression("a", context);

            Assert.IsNotNull(result);
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void EvaluateAddIntegers()
        {
            var result = EvaluateExpression("1+2");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateAddAndMultiplyIntegers()
        {
            var result = EvaluateExpression("1+2*3");

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void EvaluateAddAndMultiplyIntegersWithParenthesis()
        {
            var result = EvaluateExpression("(1+2)*3");

            Assert.IsNotNull(result);
            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void EvaluateAssignCommands()
        {
            Context context = new Context();
            EvaluateCommands("a=1\nb=2", context);

            Assert.AreEqual(1, context.GetValue("a"));
            Assert.AreEqual(2, context.GetValue("b"));
        }

        private static object EvaluateExpression(string text, Context context = null)
        {
            var parser = new Parser(text);
            var expr = parser.ParseExpression();
            return expr.Evaluate(context);
        }

        private static void EvaluateCommands(string text, Context context = null)
        {
            var parser = new Parser(text);
            var cmds = parser.ParseCommands();
            cmds.Execute(context);
        }
    }
}
