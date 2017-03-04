using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Isolate_Object_Dependencey.Tests
{
    [TestClass()]
    public class ValidationTests
    {
        [TestMethod()]
        public void CheckAuthenticationTest()
        {
            Validation target = new MyValidation();

            string id = "id anyone";
            string password = "who care";

            bool expected = true;
            bool actual;
            actual = target.CheckAuthentication(id, password);
            Assert.AreEqual(expected,actual);
        }
    }
}