namespace BScript.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Tests.Classes;
    using BScript.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ObjectUtilitiesTests
    {
        [TestMethod]
        public void GetPropertyFromString()
        {
            Assert.AreEqual(3, ObjectUtilities.GetValue("foo", "Length"));
        }

        [TestMethod]
        public void GetValueUsingCall()
        {
            Assert.AreEqual("oo", ObjectUtilities.GetValue("foo", "Substring", new object[] { 1 }));
        }

        [TestMethod]
        public void IsNumber()
        {
            Assert.IsTrue(ObjectUtilities.IsNumber((byte)1));
            Assert.IsTrue(ObjectUtilities.IsNumber((short)2));
            Assert.IsTrue(ObjectUtilities.IsNumber((int)3));
            Assert.IsTrue(ObjectUtilities.IsNumber((long)4));
            Assert.IsTrue(ObjectUtilities.IsNumber((float)1.2));
            Assert.IsTrue(ObjectUtilities.IsNumber((double)2.3));

            Assert.IsFalse(ObjectUtilities.IsNumber(null));
            Assert.IsFalse(ObjectUtilities.IsNumber("foo"));
            Assert.IsFalse(ObjectUtilities.IsNumber('a'));
            Assert.IsFalse(ObjectUtilities.IsNumber(this));
        }

        [TestMethod]
        public void SetValue()
        {
            Person person = new Person();

            ObjectUtilities.SetValue(person, "FirstName", "Adam");

            Assert.AreEqual("Adam", person.FirstName);
        }

        [TestMethod]
        public void GetNamesNativeObject()
        {
            var person = new Person();
            var result = ObjectUtilities.GetNames(person);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<string>));

            var names = (IList<string>)result;

            Assert.IsTrue(names.Contains("FirstName"));
            Assert.IsTrue(names.Contains("LastName"));
            Assert.IsTrue(names.Contains("GetName"));
            Assert.IsTrue(names.Contains("NameEvent"));
        }

        private object DummyFunction(IList<object> arguments)
        {
            return null;
        }
    }
}
