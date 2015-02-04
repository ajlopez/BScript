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
    public class WhileCommandTests
    {
        [TestMethod]
        public void CreateWhileCommand()
        {
            IExpression cond = new ConstantExpression(1);
            ICommand cmd = new ExpressionCommand(new ConstantExpression(2));
            WhileCommand wcmd = new WhileCommand(cond, cmd);

            Assert.IsNotNull(wcmd.Condition);
            Assert.AreSame(cond, wcmd.Condition);
            Assert.IsNotNull(wcmd.Command);
            Assert.AreSame(cmd, wcmd.Command);
        }

        [TestMethod]
        public void ExecuteWhileCommandWithTrueCondition()
        {
            Context context = new Context();
            IExpression cond = new NameExpression("a");
            ICommand cmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(false)));
            WhileCommand wcmd = new WhileCommand(cond, cmd);

            context.SetValue("a", true);

            wcmd.Execute(context);

            Assert.AreEqual(false, context.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteWhileCommandWithNullCondition()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(null);
            ICommand cmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            WhileCommand wcmd = new WhileCommand(cond, cmd);

            wcmd.Execute(context);

            Assert.IsNull(context.GetValue("a"));
        }
    }
}
