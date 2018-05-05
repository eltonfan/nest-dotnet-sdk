using System;
using System.IO;
using Elton.Nest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elton.Nest.Tests.Models
{
    public class DummyCallback : Callback
    {
        public Exception exception = null;
        public bool success = false;


        public void onSuccess()
        {
            success = true;
        }


        public void onFailure(NestException exception)
        {
            this.exception = exception;
        }
    }
}