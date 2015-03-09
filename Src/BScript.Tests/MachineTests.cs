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
    }
}
