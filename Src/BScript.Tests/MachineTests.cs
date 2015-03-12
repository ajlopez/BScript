﻿namespace BScript.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Files", "Files")]
    public class MachineTests
    {
        [TestMethod]
        public void CreateWithRootContext()
        {
            var machine = new Machine();

            Assert.IsNotNull(machine.RootContext);
        }

        [TestMethod]
        public void ExecuteCodeSetVariable()
        {
            var machine = new Machine();

            machine.Execute("a=1");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.IsNull(machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteCodeSetVariables()
        {
            var machine = new Machine();

            machine.Execute("a=1\nb=2");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteFileSetVariables()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\SetVariables.txt");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteFileInvokeFunction()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\InvokeFunction.txt");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }
    }
}
