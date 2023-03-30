using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Utility.Exceptions
{
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException()
        {

        }
        public RecordNotFoundException(string message):base(message)
        {

        }
        public RecordNotFoundException(string message,Exception innerException):base(message, innerException)
        {

        }
    }
}
