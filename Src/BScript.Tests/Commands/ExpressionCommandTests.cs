namespace BScript.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExpressionCommandTests
    {
        [TestMethod]
        public void CreateExpressionCommand()
        {
            IExpression expr = new ConstantExpression(1);
            ExpressionCommand cmd = new ExpressionCommand(expr);

            Assert.IsNotNull(cmd.Expression);
            Assert.AreSame(expr, cmd.Expression);
        }

        [TestMethod]
        public void ExecuteExpressionCommand()
        {
            IExpression expr = new AssignExpression("a", new ConstantExpression(1));
            ExpressionCommand cmd = new ExpressionCommand(expr);
            Context context = new Context();

            cmd.Execute(context);

            Assert.AreEqual(1, context.GetValue("a"));
        }
    }
}
