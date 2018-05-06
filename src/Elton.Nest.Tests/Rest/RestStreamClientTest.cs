using System;
using System.IO;
using System.Net.Http;
using Elton.Nest.Models;
using Elton.Nest.Rest;
using Elton.Nest.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests
{
    [TestClass]
    public class RestStreamClientTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingTokenException))]
        public void testStartWithNull_shouldThrowMissingTokenException()
        {
            var client = new RestStreamClient.Builder(new HttpClient(), new RestConfig(), new DummyParser()).build();
            client.start(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingTokenException))]
        public void testStartWithEmpty_shouldThrowMissingTokenException()
        {
            var client = new RestStreamClient.Builder(new HttpClient(), new RestConfig(), new DummyParser()).build();
            client.start("");
        }

        //private class DummyExceptionHandler : ExceptionHandler
        //{
        //    NestException exception = null;

        //    public void handle(NestException value)
        //    {
        //        exception = value;
        //    }
        //}

        //private DummyCall makeDummySuccessCall(final String message) {
        //    return new DummyCall() {

        //        public Response execute()
        // {
        //            return new Response.Builder()
        //                    .request(localHostRequest)
        //                    .protocol(Protocol.HTTP_1_0)
        //                    .code(200)
        //                    .body(new RealResponseBody(null, makeBufferedSource(message)))
        //                    .build();
        //        }
        //    };
        //}

        [TestMethod]
        public void testStart_shouldReadSuccessfully()
        {
            //final CountDownLatch latch = new CountDownLatch(1);
            //final String message = "{}";
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //DummyCall dummyCall = makeDummySuccessCall(message);
            //var parser = new DummyParser() {

            //    public void parse(String msg){
            //        super.parse(msg);
            //        latch.countDown();
            //    }
            //};

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);

            //RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), parser).build();
            //client.start("access_token");
            //latch.await();

            //Assert.IsNotNull(parser.message);
            //Assert.AreEqual(parser.message, message);
        }

        [TestMethod]
        public void testStart_shouldRaiseErrorEventWithParserException()
        {
            //final CountDownLatch latch = new CountDownLatch(1);
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //DummyCall dummyCall = makeDummySuccessCall("{}");
            //var parser = new DummyParser() {

            //    public void parse(String msg){
            //        throw new ParserException("");
            //    }
            //};

            //var handler = new DummyExceptionHandler() {

            //    public void handle(NestException value) {
            //        super.handle(value);
            //        latch.countDown();
            //    }
            //};

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);

            //RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), parser)
            //        .setExceptionHandler(handler)
            //        .build();
            //client.start("access_token");
            //latch.await();

            //Assert.IsNotNull(handler.exception);
            //Assert.IsTrue(handler.exception instanceof ParserException);
        }

        [TestMethod]
        public void testStart_shouldRaiseErrorEventWithNestException_ParsingFailure()
        {
            //final CountDownLatch latch = new CountDownLatch(1);
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //DummyCall dummyCall = makeDummySuccessCall("{}");
            //var parser = new DummyParser() {

            //    public void parse(String msg){
            //        throw new IndexOutOfBoundsException("");
            //    }
            //};

            //var handler = new DummyExceptionHandler() {

            //    public void handle(NestException value) {
            //        super.handle(value);
            //        latch.countDown();
            //    }
            //};

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);

            //RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), parser)
            //        .setExceptionHandler(handler)
            //        .build();
            //client.start("access_token");
            //latch.await();

            //Assert.IsNotNull(handler.exception);
            //Assert.IsTrue(handler.exception.Cause instanceof IndexOutOfBoundsException);
        }

        [TestMethod]
        public void testStart_shouldRaiseErrorEventWithNestException_NetworkFailure()
        {
            //Throw exception on execute() http request, validate exception is handled and retry attempted
            //   final CountDownLatch latch = new CountDownLatch(2);
            //   OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //   var dummyCall = new DummyCall() {

            //       public Response execute()
            //{
            //           throw new IOException();
            //       }
            //   };
            //   var handler = new DummyExceptionHandler() {

            //       public void handle(NestException value) {
            //           super.handle(value);
            //           latch.countDown();
            //       }
            //   };
            //   class DummyBackOff : BackOff {

            //       public long nextInterval() {
            //           latch.countDown();
            //           return 0;
            //       }


            //       public void reset() { }
            //   }

            //   PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //           .thenReturn(dummyCall);

            //   RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), new DummyParser())
            //           .setExceptionHandler(handler)
            //           .setBackOff(new DummyBackOff())
            //           .build();
            //   client.start("access_token");
            //   latch.await();

            //   Assert.IsNotNull(handler.exception);
            //   Assert.IsTrue(handler.exception.Cause instanceof IOException);
        }

        [TestMethod]
        public void testAccumulator_shouldReadUntilMessageEnds()
        {
            //Read the stream and accumulate data until end of message marker "}" or "\n"
            //Then invoke parser.parse to process
            //final CountDownLatch latch = new CountDownLatch(1);
            //final String message = "{message}";
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //DummyCall dummyCall = makeDummySuccessCall(message);
            //var parser = new DummyParser() {

            //    public void parse(String msg){
            //        super.parse(msg);
            //        latch.countDown();
            //    }
            //};

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);

            //Whitebox.setInternalState(typeof(RestStreamClient), "DEFAULT_BYTE_COUNT", 1L);

            //RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), parser).build();
            //client.start("access_token");
            //latch.await();

            //Whitebox.setInternalState(typeof(RestStreamClient), "DEFAULT_BYTE_COUNT", 2048L);

            //Assert.IsNotNull(parser.message);
            //Assert.AreEqual(parser.message, message);
        }

        [TestMethod]
        public void testStop_shouldResetBackOffStrategy()
        {
            //final CountDownLatch latch = new CountDownLatch(1);
            //final String message = "{message}";
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //DummyCall dummyCall = makeDummySuccessCall(message);
            //class DummyBackOff : BackOff {

            //    public long nextInterval() {
            //        return 0;
            //    }


            //    public void reset() {
            //        latch.countDown();
            //    }
            //}

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);
            //PowerMockito.when(mockedClient.dispatcher())
            //        .thenReturn(new Dispatcher());

            //RestStreamClient client = new RestStreamClient.Builder(mockedClient, new RestConfig(), new DummyParser())
            //        .setBackOff(new DummyBackOff())
            //        .build();
            //client.start("access_token");
            //client.stop();
            //latch.await();
        }
    }
}