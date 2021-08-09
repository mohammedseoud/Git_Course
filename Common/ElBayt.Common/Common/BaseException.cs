using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Common.Common
{
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string errorCode)
        {
            ErrorCode = errorCode;
        }

        public BaseException(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public BaseException(string errorCode, string errorMessage, object errorContext)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorContext = errorContext;
        }

        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public dynamic ErrorContext { get; set; }

        public dynamic GetSerializeObject()
        {
            return
                JsonConvert.SerializeObject(
                    new
                    {
                        ErrorType,
                        ErrorCode,
                        ErrorMessage,
                        ErrorContext
                    }
                    , new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
        }
    }
}
