using System;
using System.Net;

namespace EmployeeAccounting.Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            this.Message = message;
            this.HttpStatusCode = httpStatusCode;
        }

        public override string Message { get; }
        public HttpStatusCode HttpStatusCode { get;}
    }
}