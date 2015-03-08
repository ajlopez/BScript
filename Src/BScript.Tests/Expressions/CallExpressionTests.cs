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

        [TestMethod]
        public void EvaluateCallExpression()
        {
            Function func = new Function(new string[] { "a", "b" }, new ReturnCommand(new BinaryOperatorExpression(BinaryOperator.Add, new NameExpression("a"), new NameExpression("b"))));
            var context = new Context();
            context.SetValue("add", func);

            IList<IExpression> exprs = new List<IExpression>() { new ConstantExpression(1), new ConstantExpression(2) };
            var expr = new CallExpression("add", exprs);

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
