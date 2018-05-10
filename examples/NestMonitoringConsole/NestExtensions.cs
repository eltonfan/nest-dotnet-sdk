using Elton;
using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NestMonitoringConsole
{
    public class TokenGetter
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        readonly NestClient nestClient;
        readonly NestConfig nestConfig;
        readonly string redirectUri;
        volatile NestToken nestToken;
        public TokenGetter(NestClient nestClient, NestConfig nestConfig, string redirectUri)
        {
            this.nestClient = nestClient;
            this.nestConfig = nestConfig;
            this.redirectUri = redirectUri;

            this.nestToken = null;
        }

        public NestToken GetToken()
        {
            resetEvent.Reset();

            StartHttpListener();

            var url = NestApiUrls.GetClientCodeUrl(nestConfig.ClientId, nestConfig.StateValue, redirectUri);
            OpenBrowser(url);
            resetEvent.WaitOne();

            return this.nestToken;
        }

        static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        HttpListener httpListener = new HttpListener();
        void StartHttpListener()
        {

            var uriPrefix = new Uri(redirectUri).GetLeftPart(UriPartial.Authority) + "/";
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httpListener.Prefixes.Add(uriPrefix);
            httpListener.Start();
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    HttpListenerContext httpListenerContext = httpListener.GetContext();

                    var queryString = httpListenerContext.Request.QueryString;

                    string title = "";
                    string desc = "";
                    try
                    {
                        if (!string.IsNullOrEmpty(queryString["error"]))
                        {
                            httpListenerContext.Response.StatusCode = 200;
                            title = queryString["error"];
                            desc = queryString["error_description"];
                            continue;
                        }

                        if (string.IsNullOrEmpty(queryString["code"]))
                        {
                            httpListenerContext.Response.StatusCode = 200;
                            title = "Failed";
                            desc = "The code query is empty.";
                            continue;
                        }

                        string authorizeCode = queryString["code"];
                        log.Info("authorizeCode: " + authorizeCode);

                        this.nestToken = nestClient.CreateToken(authorizeCode);

                        log.Info("AccessToken: " + nestToken?.Token);

                        httpListenerContext.Response.StatusCode = 200;
                        title = "Finished";
                        desc = "Well done, you now have an access token which allows you to call Web API on behalf of the user.<br />Please return to the application.";
                    }
                    catch (Exception ex)
                    {
                        title = "Failed";
                        desc = "Failed to create token.<br />" + ex.StackTrace;
                        log.Error("Failed to create token.", ex);
                    }
                    finally
                    {
                        using (var writer = new StreamWriter(httpListenerContext.Response.OutputStream))
                        {
                            WriteHtml(writer, title, desc);
                        }
                    }

                    resetEvent.Set();
                }
            })).Start();
        }

        readonly string templateString = Properties.Resources.HtmlTemplate;
        void WriteHtml(StreamWriter writer, string title, string desc)
        {
            var html = templateString
                .Replace("%title%", title)
                .Replace("%desc%", desc);

            writer.Write(html);
        }
    }

    public static class NestExtensions
    {
        const string REDIRECT_URI = "http://localhost:6063/4818523d0d5a432487a85ef230b67b22";
        public static NestToken CreateToken(this NestClient nest, NestConfig config)
        {
            var getter = new TokenGetter(nest, config, REDIRECT_URI);
            return getter.GetToken();
        }
    }
}
