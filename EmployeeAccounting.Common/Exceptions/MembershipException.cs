using System.Net;

namespace EmployeeAccounting.Common.Exceptions
{
    public class MembershipException : BaseException
    {
        public MembershipException()
            : base(message, statusCode)
        {

        }

        protected MembershipException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        protected MembershipException(string message) : base(message, statusCode)
        {

        }

        private const string message = "Some employee data is not valid";
        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
    }

    public class MembershipNotFoundException : MembershipException
    {
        public MembershipNotFoundException() : base(message, statusCode)
        {

        }

        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
        private const string message = "Membership not found";
    }

    public class MembershipAlreadyExistsException : MembershipException
    {
        public MembershipAlreadyExistsException() : base(message, statusCode)
        {

        }

        private const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
        private const string message = "Membership already exists";
    }
}