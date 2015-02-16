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
    }
}
