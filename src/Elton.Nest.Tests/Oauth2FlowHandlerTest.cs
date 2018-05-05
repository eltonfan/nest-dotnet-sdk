using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    [TestClass]
    public class Oauth2FlowHandlerTest : AbstractModelTest
    {
    [TestMethod]
    public void testGetAccessTokenFromIntent_shouldReturnToken() {
        var intent = new Intent();
        var token = new NestToken("token", 123);
        intent.putExtra("access_token_key", token);

        var tokenFromIntent = new Oauth2FlowHandler(new OkHttpClient()).getAccessTokenFromIntent(intent);
        Assert.IsNotNull(tokenFromIntent);
        Assert.AreEqual(token, tokenFromIntent);
    }

    [TestMethod]
    public void testSetGetConfig_shouldWriteReadConfig() {
        var handler = new Oauth2FlowHandler(new OkHttpClient());

        String clientId = "clientId";
        String clientSecret = "clientSecret";
        String redirectUrl = "redirectUrl";
        handler.setConfig(clientId, clientSecret, redirectUrl);

        Assert.AreEqual(handler.Config.ClientID, clientId);
        Assert.AreEqual(handler.Config.ClientSecret, clientSecret);
        Assert.AreEqual(handler.Config.RedirectURL, redirectUrl);

        handler.clearConfig();

        Assert.IsNull(handler.Config);
    }

    [TestMethod]
    public void testRevokeToken_shouldRaiseOnFailureEvent_NetworkError() throws Exception {

        var callInterceptor = new DummyCall() {
            
            public void enqueue(okhttp3.Callback responseCallback) {
                responseCallback.onFailure(this, new IOException(""));
            }
        };

        var callback = new DummyCallback();

        var token = new NestToken("test-token", 12345);
        OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));
        PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
                .thenReturn(callInterceptor);

        var oauth2FlowHandler = new Oauth2FlowHandler(mockedClient);
        oauth2FlowHandler.revokeToken(token, callback);

        Assert.IsNotNull(callback.exception);
        Assert.IsTrue(callback.exception instanceof NestException);
        Assert.IsTrue(callback.exception.Cause instanceof IOException);
    }

    [TestMethod]
    public void testRevokeToken_shouldRaiseOnSuccessEvent() throws Exception {

        var callInterceptor = new DummyCall() {
            
            public void enqueue(okhttp3.Callback responseCallback) {
                Response response = new Response.Builder()
                        .request(localHostRequest)
                        .protocol(Protocol.HTTP_1_0)
                        .code(200)
                        .body(new RealResponseBody(null, makeBufferedSource("{}")))
                        .build();

                try {
                    responseCallback.onResponse(this, response);
                } catch (IOException ignore) { }
            }
        };

        var callback = new DummyCallback();

        var token = new NestToken("test-token", 12345);
        OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));
        PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
                .thenReturn(callInterceptor);

        var oauth2FlowHandler = new Oauth2FlowHandler(mockedClient);
        oauth2FlowHandler.revokeToken(token, callback);

        Assert.IsTrue(callback.success);
    }


    [TestMethod]
    public void testRevokeToken_shouldRaiseOnFailureEvent_ServerError() throws Exception {

        var callInterceptor = new DummyCall() {
            
            public void enqueue(okhttp3.Callback responseCallback) {
                Response response = new Response.Builder()
                        .request(localHostRequest)
                        .protocol(Protocol.HTTP_1_0)
                        .code(404)
                        .body(new RealResponseBody(null, makeBufferedSource("{}")))
                        .build();

                try {
                    responseCallback.onResponse(this, response);
                } catch (IOException ignore) { }
            }
        };

        var callback = new DummyCallback();

        var token = new NestToken("test-token", 12345);
        OkHttpClient mockedClient = PowerMockito.mock(typeof(OkHttpClient));
        PowerMockito.when(mockedClient.newCall(any(typeof(Request))))
                .thenReturn(callInterceptor);

        var oauth2FlowHandler = new Oauth2FlowHandler(mockedClient);
        oauth2FlowHandler.revokeToken(token, callback);

        Assert.IsNotNull(callback.exception);
        Assert.IsTrue(callback.exception instanceof ServerException);
    }
}
