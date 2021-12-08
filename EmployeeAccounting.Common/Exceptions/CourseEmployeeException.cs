using System.Net;

namespace EmployeeAccounting.Common.Exceptions
{
    public class CourseEmployeeException : BaseException
    {
        public CourseEmployeeException() : base(message, statusCode)
        {
        }

        protected CourseEmployeeException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        protected CourseEmployeeException(string message) : base(message, statusCode)
        {

        }

        private const string message = "Some employee data is not valid";
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
    }

    public class CourseEmployeeAlreadyAddedException : EmployeeException
    {
        public CourseEmployeeAlreadyAddedException() : base(message, statusCode)
        {

        }

        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
        private const string message = "This employee is already attending this course";
    }
}