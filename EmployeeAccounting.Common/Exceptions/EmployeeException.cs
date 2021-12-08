using System;
using System.Net;

namespace EmployeeAccounting.Common.Exceptions
{
    public class EmployeeException : BaseException
    {
        public EmployeeException()
            : base(message, statusCode)
        {
            
        }

        protected EmployeeException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        protected EmployeeException(string message) : base(message, statusCode)
        {

        }

        private const string message = "Some employee data is not valid";
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
    }

    public class EmployeeNotFoundException : EmployeeException
    {
        public EmployeeNotFoundException() : base(message, statusCode)
        {
            
        }

        private const HttpStatusCode statusCode = HttpStatusCode.NotFound;
        private const string message = "Employee not found";
    }
}