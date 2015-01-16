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
    public class IfCommandTests
    {
        [TestMethod]
        public void CreateIfCommand()
        {
            IExpression cond = new ConstantExpression(1);
            ICommand thencmd = new ExpressionCommand(new ConstantExpression(2));
            IfCommand cmd = new IfCommand(cond, thencmd);

            Assert.IsNotNull(cmd.Condition);
            Assert.AreSame(cond, cmd.Condition);
            Assert.IsNotNull(cmd.ThenCommand);
            Assert.AreSame(thencmd, cmd.ThenCommand);
            Assert.IsNull(cmd.ElseCommand);
        }

        [TestMethod]
        public void CreateIfCommandWithElse()
        {
            IExpression cond = new ConstantExpression(1);
            ICommand thencmd = new ExpressionCommand(new ConstantExpression(2));
            ICommand elsecmd = new ExpressionCommand(new ConstantExpression(3));
            IfCommand cmd = new IfCommand(cond, thencmd, elsecmd);

            Assert.IsNotNull(cmd.Condition);
            Assert.AreSame(cond, cmd.Condition);
            Assert.IsNotNull(cmd.ThenCommand);
            Assert.AreSame(thencmd, cmd.ThenCommand);
            Assert.IsNotNull(cmd.ElseCommand);
            Assert.AreSame(elsecmd, cmd.ElseCommand);
        }

        [TestMethod]
        public void ExecuteIfCommandWithTrueCondition()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(true);
            ICommand thencmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            IfCommand cmd = new IfCommand(cond, thencmd);

            cmd.Execute(context);

            Assert.AreEqual(42, context.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteIfCommandWithFalseCondition()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(false);
            ICommand thencmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            IfCommand cmd = new IfCommand(cond, thencmd);

            cmd.Execute(context);

            Assert.IsNull(context.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteIfCommandWithFalseConditionAndElse()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(false);
            ICommand thencmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            ICommand elsecmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(123)));
            IfCommand cmd = new IfCommand(cond, thencmd, elsecmd);

            cmd.Execute(context);

            Assert.AreEqual(123, context.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteIfCommandWithNullCondition()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(null);
            ICommand thencmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            IfCommand cmd = new IfCommand(cond, thencmd);

            cmd.Execute(context);

            Assert.IsNull(context.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteIfCommandWithNullConditionAndElse()
        {
            Context context = new Context();
            IExpression cond = new ConstantExpression(null);
            ICommand thencmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(42)));
            ICommand elsecmd = new ExpressionCommand(new AssignExpression("a", new ConstantExpression(123)));
            IfCommand cmd = new IfCommand(cond, thencmd, elsecmd);

            cmd.Execute(context);

            Assert.AreEqual(123, context.GetValue("a"));
        }
    }
}
