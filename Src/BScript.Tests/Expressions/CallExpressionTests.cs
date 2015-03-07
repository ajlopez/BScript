namespace BScript.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
using BScript.Expressions;

    [TestClass]
    public class CallExpressionTests
    {
        [TestMethod]
        public void CreateCallExpression()
        {
            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(1), new ConstantExpression(2) };

            var expr = new CallExpression("foo", exprs);

            Assert.AreEqual("foo", expr.Name);
            Assert.AreSame(exprs, expr.ArgumentExpressions);
        }
    }
}
