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
    public class CallDotExpressionTests
    {
        [TestMethod]
        public void CreateCallDotExpression()
        {
            NameExpression nexpr = new NameExpression("foo");
            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(1), new ConstantExpression(2) };

            var expr = new CallDotExpression(nexpr, exprs);

            Assert.AreEqual(nexpr, expr.Expression);
            Assert.AreSame(exprs, expr.ArgumentExpressions);
        }
    }
}
