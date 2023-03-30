using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Utility.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        public DuplicateRecordException()
        {

        }
        public DuplicateRecordException(string message) : base(message)
        {

        }
        public DuplicateRecordException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
