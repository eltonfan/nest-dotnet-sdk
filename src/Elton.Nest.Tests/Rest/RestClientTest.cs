using System;
using System.IO;
using System.Net.Http;
using Elton.Nest.Models;
using Elton.Nest.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class RestClientTest : AbstractModelTest
    {

        [TestMethod]
        [ExpectedException(typeof(MissingTokenException))]
        public void testSetTokenNull_expectMissingTokenException()
        {
            var client = new RestClient(new HttpClient(), new RestConfig(), new DummyParser());
            client.setToken(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingTokenException))]
        public void testSetTokenEmpty_expectMissingTokenException()
        {
            var client = new RestClient(new HttpClient(), new RestConfig(), new DummyParser());
            client.setToken("");
        }

        [TestMethod]
        [ExpectedException(typeof(MissingTokenException))]
        public void testWrite_expectMissingTokenException()
        {
            var client = new RestClient(new HttpClient(), new RestConfig(), new DummyParser());
            client.writeDouble("path", "field", 0f, null);
        }

        [TestMethod]
        public void testWrite_shouldResetRedirectUrl()
        {
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //var dummyUnknownHostExceptionCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);
            //        responseCallback.onFailure(this, new UnknownHostException());
            //    }
            //};
            //var dummyIOExceptionCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);
            //        responseCallback.onFailure(this, new IOException());
            //    }
            //};

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyUnknownHostExceptionCall)
            //        .thenReturn(dummyIOExceptionCall);

            //var client = new RestClient(mockedClient, new RestConfig(), new DummyParser());
            //Whitebox.setInternalState(client, "redirectApiUrl", "http://localhost");
            //Assert.AreEqual(Whitebox.getInternalState(client, "redirectApiUrl"), "http://localhost");

            //client.setToken("access_token");
            //client.writeBoolean("path", "field", true, null);

            //Assert.IsTrue(dummyUnknownHostExceptionCall.visited);
            //Assert.IsTrue(dummyIOExceptionCall.visited);
            //Assert.AreEqual(Whitebox.getInternalState(client, "redirectApiUrl"), null);
        }

        [TestMethod]
        public void testWrite_shouldFailWithServerException()
        {
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //var dummyCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);
            //        Response response = new Response.Builder()
            //                .request(localHostRequest)
            //                .protocol(Protocol.HTTP_1_0)
            //                .code(500)
            //                .body(new RealResponseBody(null, null))
            //                .build();

            //        try {
            //            responseCallback.onResponse(this, response);
            //        } catch (IOException ignore) { }
            //    }
            //};

            //var dummyCallback = new DummyCallback();

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyCall);

            //var client = new RestClient(mockedClient, new RestConfig(), new DummyParser());
            //client.setToken("access_token");
            //client.writeLong("path", "field", 0, dummyCallback);

            //Assert.IsTrue(dummyCall.visited);
            //Assert.IsNotNull(dummyCallback.exception);
            //Assert.IsTrue(dummyCallback.exception instanceof ServerException);
        }

        [TestMethod]
        public void testWrite_shouldSwitchToRedirectUrl()
        {
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //final String redirectUrl = "http://127.0.0.1";
            //var dummyRedirectCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);

            //        Response response = new Response.Builder()
            //                .request(localHostRequest)
            //                .protocol(Protocol.HTTP_1_0)
            //                .code(307)
            //                .addHeader("Location", redirectUrl)
            //                .body(new RealResponseBody(null, null))
            //                .build();

            //        try {
            //            responseCallback.onResponse(this, response);
            //        } catch (IOException ignore) { }
            //    }
            //};

            //var dummySuccessCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);

            //        Response response = new Response.Builder()
            //                .request(localHostRequest)
            //                .protocol(Protocol.HTTP_1_0)
            //                .code(200)
            //                .body(new RealResponseBody(null, makeBufferedSource("{}")))
            //                .build();

            //        try {
            //            responseCallback.onResponse(this, response);
            //        } catch (IOException ignore) { }
            //    }
            //};

            //var dummyCallback = new DummyCallback();

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyRedirectCall)
            //        .thenReturn(dummySuccessCall);

            //var client = new RestClient(mockedClient, new RestConfig(), new DummyParser());
            //client.setToken("access_token");
            //client.writeLong("path", "field", 0, dummyCallback);

            //Assert.IsTrue(dummyCallback.success);
            //Assert.AreEqual(Whitebox.getInternalState(client, "redirectApiUrl"), redirectUrl);
        }

        [TestMethod]
        public void testWrite_shouldReturnMalformedException()
        {
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //final String redirectUrl = "invalid_url";
            //var dummyRedirectCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);

            //        Response response = new Response.Builder()
            //                .request(localHostRequest)
            //                .protocol(Protocol.HTTP_1_0)
            //                .code(307)
            //                .addHeader("Location", redirectUrl)
            //                .body(new RealResponseBody(null, null))
            //                .build();

            //        try {
            //            responseCallback.onResponse(this, response);
            //        } catch (IOException ignore) { }
            //    }
            //};

            //var dummyCallback = new DummyCallback();

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummyRedirectCall);

            //var client = new RestClient(mockedClient, new RestConfig(), new DummyParser());
            //client.setToken("access_token");
            //client.writeLong("path", "field", 0, dummyCallback);

            //Assert.IsNotNull(dummyCallback.exception);
            //Assert.IsTrue(dummyCallback.exception instanceof NestException);
            //Assert.IsNotNull(dummyCallback.exception.Cause);
            //Assert.IsTrue(dummyCallback.exception.Cause instanceof MalformedURLException);
        }

        [TestMethod]
        public void testWrite_shouldReturnParserException()
        {
            //OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));

            //var dummySuccessCall = new DummyCall() {

            //    public void enqueue(Callback responseCallback) {
            //        super.enqueue(responseCallback);

            //        Response response = new Response.Builder()
            //                .request(localHostRequest)
            //                .protocol(Protocol.HTTP_1_0)
            //                .code(200)
            //                .body(new RealResponseBody(null, makeBufferedSource("{}")))
            //                .build();

            //        try {
            //            responseCallback.onResponse(this, response);
            //        } catch (IOException ignore) { }
            //    }
            //};

            //var dummyCallback = new DummyCallback();

            //PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
            //        .thenReturn(dummySuccessCall);

            //var client = new RestClient(mockedClient, new RestConfig(), new Parser(){

            //    public void parse(String msg){
            //        throw new ParserException("");
            //    }
            //});
            //client.setToken("access_token");
            //client.writeLong("path", "field", 0, dummyCallback);

            //Assert.IsNotNull(dummyCallback.exception);
            //Assert.IsTrue(dummyCallback.exception instanceof ParserException);
        }

    }
}