namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
