namespace BScript.Tests.Commands
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Expressions;
    using BScript.Commands;

    [TestClass]
    [DeploymentItem("Files", "Files")]
    public class IncludeCommandTests
    {
        [TestMethod]
        public void IncludeFile()
        {
            IncludeCommand cmd = new IncludeCommand(new ConstantExpression("Files\\SetVariables.txt"));
            Context context = new Context();

            cmd.Execute(context);

            Assert.AreEqual(1, context.GetValue("a"));
            Assert.AreEqual(2, context.GetValue("b"));
        }
    }
}
