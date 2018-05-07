using System;
using System.IO;
using Elton.Nest.Models;
using Elton.Nest.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class FibonacciBackOffTest : AbstractModelTest
    {
        [TestMethod]
        public void testNextInterval_shouldReturnCorrectSequence()
        {
            FibonacciBackOff backOff = new FibonacciBackOff.Builder()
                    .setInitialDelayMillis(1)
                    .setMaxDelayMillis(7)
                    .build();

            //According to Fibonacci seq. last value should be 8, but we capped it at 7
            long[] values = { 1, 1, 2, 3, 5, 7 };
            foreach (long v in values)
            {
                Assert.AreEqual(backOff.NextInterval(), v);
            }

            //Reset and retry
            backOff.Reset();
            foreach (long v in values)
            {
                Assert.AreEqual(backOff.NextInterval(), v);
            }
        }
    }
}