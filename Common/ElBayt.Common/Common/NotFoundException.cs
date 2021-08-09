using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Common
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public NotFoundException(string errorCode, string errorMessage, object errorContext) : base(errorCode, errorMessage, errorContext)
        {
        }
    }
}
