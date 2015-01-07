namespace BScript.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetUndefinedValue()
        {
            Context context = new Context();

            Assert.IsNull(context.GetValue("Unknown"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();

            context.SetValue("Answer", 42);
            Assert.AreEqual(42, context.GetValue("Answer"));
        }
    }
}
