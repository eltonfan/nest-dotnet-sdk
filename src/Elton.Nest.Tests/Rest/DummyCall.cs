using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    public class DummyCall : Call
    {
        public Boolean visited = false;

        public static Request localHostRequest = new Request.Builder().url("http://localhost").build();
        public static BufferedSource makeBufferedSource(String msg)
        {
            try
            {
                InputStream input = IOUtils.toInputStream(msg, "UTF-8");
                return Okio.buffer(Okio.source(input));
            }
            catch (IOException ignore) { }

            return null;
        }


        public Request request()
        {
            return null;
        }


        public Response execute()
        {
            return null;
        }


        public void enqueue(Callback responseCallback)
        {
            visited = true;
        }


        public void cancel() { }


        public bool isExecuted()
        {
            return false;
        }


        public bool isCanceled()
        {
            return false;
        }


        public Call clone()
        {
            return null;
        }
    }
}