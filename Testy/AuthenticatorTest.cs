using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using libcommon;
using Testy.Auth;

namespace Testy
{
    /// <summary>
    /// Summary description for AuthenticatorTest
    /// </summary>
    [TestClass]
    public class AuthenticatorTest
    {

        private UserDBMock userDb;

        public AuthenticatorTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            userDb = new UserDBMock();
        }

        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void User1Login()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("Foo", "pomoc");
        }

        [TestMethod]
        public void UserInfo1()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("Foo", "pomoc");
            Assert.IsTrue(u.ID == 1);
            Assert.IsTrue(u.Role == "Admin");
            Assert.IsTrue(u.DisplayName == "Foo Bar");
        }

        [TestMethod]
        public void User2Login()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("Yar", "pejsek");
        }

        [TestMethod]
        public void UserInfo2()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("Yar", "pejsek");
            Assert.IsTrue(u.ID == 2);
            Assert.IsTrue(u.Role == "Member");
            Assert.IsTrue(u.DisplayName == "Yaros Takos");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserException),
        "Non-existing user did not cause exception")]
        public void UserNotFound()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("ASDASDASDWDFASFAd", "pomoc");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPasswordException),
        "User was logged in using wrong password")]
        public void BadPassword()
        {
            var auth = new Authenticator(userDb);
            var u = auth.Login("Foo", "afjsdafsjfs");
        }

    }
}
