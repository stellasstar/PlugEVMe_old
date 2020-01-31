using System;
using System.Collections.Generic;
using System.Text;

namespace PlugEVMe.Exceptions
{

    public class AppException : Exception
    {
        public AppException(String message) : base(message)
        { }

        public AppException(String message, Exception inner) : base(message, inner) { }
    }

}