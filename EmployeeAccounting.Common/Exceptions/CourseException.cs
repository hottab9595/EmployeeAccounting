using System.Net;

namespace EmployeeAccounting.Common.Exceptions
{
    public class CourseException : BaseException
    {
        public CourseException()
            : base(message, statusCode)
        {

        }

        protected CourseException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        protected CourseException(string message) : base(message, statusCode)
        {

        }

        private const string message = "Some course data is not valid";
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
    }

    public class CourseNotFoundException : CourseException
    {
        public CourseNotFoundException() : base(message, statusCode)
        {

        }

        private const HttpStatusCode statusCode = HttpStatusCode.NotFound;
        private const string message = "Course not found";
    }
}