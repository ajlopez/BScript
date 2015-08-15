namespace BScript.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BScript.Language;

    [TestClass]
    public class PrintTests
    {
        [TestMethod]
        public void PrintString()
        {
            Machine machine = new Machine();
            StringWriter writer = new StringWriter();
            machine.Out = writer;

            Print print = new Print(machine);

            Assert.IsNull(print.Evaluate(null, new object[] { "foo" }));

            Assert.AreEqual("foo", writer.ToString());
        }
    }
}
