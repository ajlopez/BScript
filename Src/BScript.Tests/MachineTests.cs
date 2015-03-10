namespace BScript.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
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
    }
}
