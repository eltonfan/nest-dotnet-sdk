using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class NestExceptionTest : AbstractModelTest
    {
        [TestMethod]
        public void testNestException_shouldExtendException()
        {
            Assert.AreEqual(typeof(NestException).BaseType, typeof(Exception));
        }

        [TestMethod]
        public void testNestException_shouldAcceptAMessage()
        {
            string testMessage = "test-message";
            var e = new NestException(testMessage);

            Assert.AreEqual(testMessage, e.Message);
        }

        [TestMethod]
        public void testNestException_shouldAcceptNothing()
        {
            var e = new NestException();

            Assert.IsTrue(string.IsNullOrEmpty(e.Message));
        }

        [TestMethod]
        public void testNestException_shouldAcceptAThrowable()
        {
            var t = new Exception();
            var e = new NestException(t);

            Assert.AreSame(t, e.InnerException);
        }

        [TestMethod]
        public void testNestException_shouldAcceptAMessageAndThrowable()
        {
            string testMessage = "test-message-2";
            var t = new Exception();
            var e = new NestException(testMessage, t);

            Assert.AreEqual(testMessage, e.Message);
            Assert.AreSame(t, e.InnerException);
        }

    }
}