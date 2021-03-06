﻿namespace BScript.Tests
{
    using System;
    using System.IO;
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
        public void CreateWithMachineVariable()
        {
            var machine = new Machine();

            var result = machine.RootContext.GetValue("machine");

            Assert.IsNotNull(result);
            Assert.AreSame(machine, result);
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
        public void ExecuteFileInclude()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\Include.txt");

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

        [TestMethod]
        public void ExecuteFileWhile()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\While.txt");

            Assert.AreEqual(10, machine.RootContext.GetValue("a"));
            Assert.AreEqual(5, machine.RootContext.GetValue("k"));
        }

        [TestMethod]
        public void ExecuteFileIf()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\If.txt");

            Assert.AreEqual(2, machine.RootContext.GetValue("a"));
            Assert.AreEqual(1, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteFileFor()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\For.txt");

            Assert.AreEqual(10, machine.RootContext.GetValue("a"));
            Assert.AreEqual(5, machine.RootContext.GetValue("k"));
        }

        [TestMethod]
        public void ExecuteFileNew()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\New.txt");

            var result = machine.RootContext.GetValue("context");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Context));

            var context = (Context)result;

            Assert.IsNotNull(context.Parent);
            Assert.AreSame(context.Parent, machine.RootContext.GetValue("parent"));
        }

        [TestMethod]
        public void ExecuteFileForWithStep()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\ForWithStep.txt");

            Assert.AreEqual(9, machine.RootContext.GetValue("a"));
            Assert.AreEqual(7, machine.RootContext.GetValue("k"));
        }

        [TestMethod]
        public void ExecuteFileInvokeFunctionBeforeDefinition()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\InvokeFunctionBeforeDefinition.txt");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteFileFactorial()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\Factorial.txt");

            Assert.AreEqual(1, machine.RootContext.GetValue("f1"));
            Assert.AreEqual(2, machine.RootContext.GetValue("f2"));
            Assert.AreEqual(6, machine.RootContext.GetValue("f3"));
            Assert.AreEqual(24, machine.RootContext.GetValue("f4"));
        }

        [TestMethod]
        public void ExecuteFileMachineContext()
        {
            var machine = new Machine();

            machine.ExecuteFile("Files\\MachineContext.txt");

            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(1, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void InitialOutputPointsToConsoleOut()
        {
            var machine = new Machine();

            Assert.AreSame(System.Console.Out, machine.Out);
        }

        [TestMethod]
        public void PrintHello()
        {
            var machine = new Machine();
            var sw = new StringWriter();
            machine.Out = sw;

            machine.Execute("print(\"Hello, world\")");

            Assert.AreEqual("Hello, world", sw.ToString());
        }
    }
}
