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
    public class CompositeCommandTests
    {
        [TestMethod]
        public void CreateCompositeCommand()
        {
            ICommand cmd1 = new ExpressionCommand(new ConstantExpression(1));
            ICommand cmd2 = new ExpressionCommand(new ConstantExpression(2));
            IList<ICommand> cmds = new ICommand[] { cmd1, cmd2 };

            CompositeCommand cmd = new CompositeCommand(cmds);

            Assert.IsNotNull(cmd.Commands);
            Assert.AreSame(cmds, cmd.Commands);
        }

        [TestMethod]
        public void ExecuteCompositeCommand()
        {
            IExpression expr1 = new AssignExpression("a", new ConstantExpression(1));
            ExpressionCommand cmd1 = new ExpressionCommand(expr1);
            IExpression expr2 = new AssignExpression("b", new ConstantExpression(2));
            ExpressionCommand cmd2 = new ExpressionCommand(expr2);
            IList<ICommand> cmds = new ICommand[] { cmd1, cmd2 };

            CompositeCommand cmd = new CompositeCommand(cmds);

            Context context = new Context();

            cmd.Execute(context);

            Assert.AreEqual(1, context.GetValue("a"));
            Assert.AreEqual(2, context.GetValue("b"));
        }
    }
}
