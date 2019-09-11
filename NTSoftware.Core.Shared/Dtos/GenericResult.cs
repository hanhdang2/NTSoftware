using System;
using System.Collections.Generic;
using System.Text;

namespace NTSoftware.Core.Shared.Dtos
{
    public class GenericResult
    {
        public GenericResult()
        {
        }

        public GenericResult(bool success)
        {
            Success = success;
        }

        public GenericResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public GenericResult(bool success, object data)
        {
            Success = success;
            Data = data;
        }

        public GenericResult(object data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
        public GenericResult(object data, bool success, string message, int errorCode)
        {
            Data = data;
            Success = success;
            Message = message;
            ErrorCode = errorCode;
        }
        public object Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
        public int ErrorCode { get; set; }

    }
}
