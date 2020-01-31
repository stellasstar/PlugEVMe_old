using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Diagnostics;
using Xamarin.Forms;

namespace PlugEVMe.Exceptions
{
    [Serializable]
    public class NoConnectionException : Exception
    {
        public NoConnectionException(Exception innerException)  { }

        public NoConnectionException(string message) : base(message) { }

        public NoConnectionException(string message, Exception innerException) : base(message, innerException) { }

        protected NoConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
