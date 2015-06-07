using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ServiceStack;
using WindowsService1.ServiceModel;
using ServiceStack.Messaging;

namespace WindowsService1.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            throw new HelloException("testing 1..2..3..");

            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }

/*    public class HelloException : MessagingException
    {
        public HelloException()
        {
        }

        public HelloException(string message)
            : base(message)
        {
        }

        public HelloException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }*/

    public class HelloException : Exception
    {
        public HelloException()
        {
        }

        public HelloException(string message)
            : base(message)
        {
        }

        public HelloException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}