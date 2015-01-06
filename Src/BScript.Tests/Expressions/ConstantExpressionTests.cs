namespace BScript.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Expressions;

    [TestClass]
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void CreateIntegerConstant()
        {
            var expr = new ConstantExpression(42);

            Assert.AreEqual(42, expr.Value);
            Assert.AreEqual(42, expr.Evaluate());
        }
    }
}
