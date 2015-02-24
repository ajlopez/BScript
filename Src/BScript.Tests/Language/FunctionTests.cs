namespace BScript.Tests.Language
{
    using System;
    using BScript.Commands;
    using BScript.Expressions;
    using BScript.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void CreateFunction()
        {
            ICommand body = new ExpressionCommand(new ConstantExpression(1));
            Function fn = new Function(new string[] { "a", "b" }, body);

            Assert.IsNotNull(fn.ArgumentNames);
            Assert.AreEqual(2, fn.ArgumentNames.Count);
            Assert.AreEqual("a", fn.ArgumentNames[0]);
            Assert.AreEqual("b", fn.ArgumentNames[1]);
            Assert.IsNotNull(fn.Body);
            Assert.AreSame(body, fn.Body);
        }

        [TestMethod]
        public void ExecuteFunction()
        {
            ICommand body = new ReturnCommand(new BinaryOperatorExpression(BinaryOperator.Add, new NameExpression("a"), new NameExpression("b")));
            Function fn = new Function(new string[] { "a", "b" }, body);

            var result = fn.Evaluate(null, new object[] { 1, 2 });

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void ExecuteFunctionUsingExternalVariable()
        {
            ICommand body = new ReturnCommand(new BinaryOperatorExpression(BinaryOperator.Add, new NameExpression("a"), new NameExpression("b")));
            Function fn = new Function(new string[] { "a" }, body);
            Context context = new Context();

            context.SetValue("b", 2);

            var result = fn.Evaluate(context, new object[] { 1 });

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
