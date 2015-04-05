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
            DotExpression dotexpr = new DotExpression(new NameExpression("foo"), "bar");
            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(1), new ConstantExpression(2) };

            var expr = new CallDotExpression(dotexpr, exprs);

            Assert.AreEqual(dotexpr, expr.Expression);
            Assert.AreSame(exprs, expr.ArgumentExpressions);
        }
    }
}
