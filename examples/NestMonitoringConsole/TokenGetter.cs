using Elton;
using Elton.Nest;
using Elton.Nest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NestMonitoringConsole
{
    public class TokenGetter : IDisposable
    {
        static readonly Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        readonly AutoResetEvent resetEvent = new AutoResetEvent(false);

        readonly NestConfig nestConfig;
        readonly NestOauth2 oauth2;
        volatile NestToken nestToken;
        public TokenGetter(NestConfig nestConfig)
        {
            this.oauth2 = new NestOauth2(nestConfig);
            this.nestConfig = nestConfig;

            this.nestToken = null;
        }

        public NestToken GetToken()
        {
            resetEvent.Reset();

            StartHttpListener();

            var url = oauth2.GetClientCodeUrl(nestConfig.RedirectUrl);
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
            var uriPrefix = new Uri(nestConfig.RedirectUrl).GetLeftPart(UriPartial.Authority) + "/";
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httpListener.Prefixes.Add(uriPrefix);
            httpListener.Start();
            new Thread(() =>
            {
                while (true)
                {
                    HttpListenerContext httpListenerContext = httpListener.GetContext();

                    var queryString = httpListenerContext.Request.QueryString;

                    try
                    {
                        var code = oauth2.ParseAuthorizationCode(httpListenerContext.Request.Url.OriginalString);
                        this.nestToken = oauth2.CreateToken(code);

                        log.Info("AccessToken: " + nestToken?.Token);

                        httpListenerContext.Response.StatusCode = 200;
                        using (var writer = new StreamWriter(httpListenerContext.Response.OutputStream))
                        {
                            WriteHtml(writer,
                                "Finished",
                                "Well done, you now have an access token which allows you to call Web API on behalf of the user.<br />Please return to the application.");
                        }

                        resetEvent.Set();
                        break;
                    }
                    catch (Exception ex)
                    {
                        log.Error("Failed to create token.", ex);
                        using (var writer = new StreamWriter(httpListenerContext.Response.OutputStream))
                        {
                            WriteHtml(writer,
                                "Failed",
                                "Failed to create token.<br />" + ex.StackTrace);
                        }
                    }
                }
            }).Start();
        }

        readonly string templateString = Properties.Resources.HtmlTemplate;
        void WriteHtml(StreamWriter writer, string title, string desc)
        {
            var html = templateString
                .Replace("%title%", title)
                .Replace("%desc%", desc);

            writer.Write(html);
        }

        public void Dispose()
        {
            oauth2?.Dispose();
            httpListener?.Stop();
        }
    }
}
