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
    public class ForCommandTests
    {
        [TestMethod]
        public void CreateForCommand()
        {
            IExpression fromexpr = new ConstantExpression(1);
            IExpression toexpr = new ConstantExpression(2);
            ICommand body = new ExpressionCommand(new ConstantExpression(2));
            ForCommand fcmd = new ForCommand("k", fromexpr, toexpr, body);

            Assert.AreEqual("k", fcmd.Name);
            Assert.AreSame(fromexpr, fcmd.FromExpression);
            Assert.AreSame(toexpr, fcmd.ToExpression);
            Assert.AreSame(body, fcmd.Body);
        }

        [TestMethod]
        public void ExecuteForCommand()
        {
            IExpression fromexpr = new ConstantExpression(1);
            IExpression toexpr = new ConstantExpression(4);
            ICommand body = new ExpressionCommand(new AssignExpression("a", new BinaryOperatorExpression(BinaryOperator.Multiply, new NameExpression("a"), new NameExpression("k"))));
            ForCommand fcmd = new ForCommand("k", fromexpr, toexpr, body);

            Context context = new Context();
            context.SetValue("a", 1);

            fcmd.Execute(context);

            Assert.AreEqual(24, context.GetValue("a"));
        }

        [TestMethod]
        public void CreateForCommandWithStep()
        {
            IExpression fromexpr = new ConstantExpression(1);
            IExpression toexpr = new ConstantExpression(2);
            IExpression stepexpr = new ConstantExpression(2);
            ICommand body = new ExpressionCommand(new ConstantExpression(2));
            ForCommand fcmd = new ForCommand("k", fromexpr, toexpr, stepexpr, body);

            Assert.AreEqual("k", fcmd.Name);
            Assert.AreSame(fromexpr, fcmd.FromExpression);
            Assert.AreSame(toexpr, fcmd.ToExpression);
            Assert.AreSame(stepexpr, fcmd.StepExpression);
            Assert.AreSame(body, fcmd.Body);
        }

        [TestMethod]
        public void ExecuteForCommandWithStep()
        {
            IExpression fromexpr = new ConstantExpression(1);
            IExpression toexpr = new ConstantExpression(4);
            IExpression stepexpr = new ConstantExpression(2);
            ICommand body = new ExpressionCommand(new AssignExpression("a", new BinaryOperatorExpression(BinaryOperator.Multiply, new NameExpression("a"), new NameExpression("k"))));
            ForCommand fcmd = new ForCommand("k", fromexpr, toexpr, stepexpr, body);

            Context context = new Context();
            context.SetValue("a", 1);

            fcmd.Execute(context);

            Assert.AreEqual(3, context.GetValue("a"));
        }
    }
}
