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

        private static object EvaluateExpression(string text)
        {
            var parser = new Parser(text);
            var expr = parser.ParseExpression();
            return expr.Evaluate(null);
        }
    }
}
