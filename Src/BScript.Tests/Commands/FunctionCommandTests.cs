namespace BScript.Tests.Commands
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
    public class FunctionCommandTests
    {
        [TestMethod]
        public void CreateFunctionCommand()
        {
            string name = "foo";
            IList<string> argnames = new string[] { "a", "b" };
            ICommand body = new ExpressionCommand(new ConstantExpression(2));

            FunctionCommand fcmd = new FunctionCommand(name, argnames, body);

            Assert.AreEqual(name, fcmd.Name);
            Assert.AreSame(argnames, fcmd.ArgumentNames);
            Assert.AreSame(body, fcmd.Body);
        }

        [TestMethod]
        public void EvaluateFunctionCommand()
        {
            string name = "foo";
            IList<string> argnames = new string[] { "a", "b" };
            ICommand body = new ExpressionCommand(new ConstantExpression(2));
            Context context = new Context();

            FunctionCommand fcmd = new FunctionCommand(name, argnames, body);

            fcmd.Execute(context);

            var result = context.GetValue("foo");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Function));
        }
    }
}
