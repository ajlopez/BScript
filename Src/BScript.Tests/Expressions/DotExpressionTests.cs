namespace BScript.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Expressions;
    using BScript.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DotExpressionTests
    {
        [TestMethod]
        public void CreateDotExpression()
        {
            IExpression lexpr = new ConstantExpression(1);

            var expr = new DotExpression(lexpr, "Foo");

            Assert.AreEqual("Foo", expr.Name);
            Assert.AreSame(lexpr, expr.Expression);
        }
    }
}
