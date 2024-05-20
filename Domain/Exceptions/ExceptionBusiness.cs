using System;
namespace Domain.Exceptions
{
    public class ExceptionBusiness : Exception
    {
        public ExceptionBusiness()
        {
        }

        public ExceptionBusiness(string message) : base(message)
        {
        }

        public ExceptionBusiness(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string ToString() => Message;
    }
}

