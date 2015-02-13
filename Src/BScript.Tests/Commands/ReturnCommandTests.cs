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
    public class ReturnCommandTests
    {
        [TestMethod]
        public void CreateReturnCommand()
        {
            IExpression expr = new ConstantExpression(1);
            ReturnCommand cmd = new ReturnCommand(expr);

            Assert.IsNotNull(cmd.Expression);
            Assert.AreSame(expr, cmd.Expression);
        }

        [TestMethod]
        public void ExecuteReturnCommand()
        {
            IExpression expr = new ConstantExpression(1);
            ReturnCommand cmd = new ReturnCommand(expr);
            Context context = new Context();

            cmd.Execute(context);

            Assert.IsTrue(context.HasReturn);
            Assert.AreEqual(1, context.ReturnValue);
        }

        [TestMethod]
        public void ExecuteReturnCommandWithoutExpression()
        {
            ReturnCommand cmd = new ReturnCommand(null);
            Context context = new Context();

            cmd.Execute(context);

            Assert.IsTrue(context.HasReturn);
            Assert.IsNull(context.ReturnValue);
        }
    }
}
