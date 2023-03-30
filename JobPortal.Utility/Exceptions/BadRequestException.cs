using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace JobPortal.Utility.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {

        }
        public BadRequestException(string message) : base(message)
        {

        }
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
