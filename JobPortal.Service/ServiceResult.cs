﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPortal.Service
{
    public class ServiceResult<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; }
        public T Data { get; set; }

        public ServiceResult()
        {
            Status = 404;
            Message = "No Records";
        }
        public void SetSuccess()
        {
            Status = 200;
            Message= "Success";
        }
        public void SetSuccess(T data)
        {
            this.Data = data;
            Status = 200;
            Message = "Success";
        }
        public void SetFailure(string failureMessage)
        {
            Status = 417;
            Message = failureMessage;
        }
        public void SetAccessDenied()
        {
            Status = 703;
            Message = "Access Denied";
        }
        public void SetStatus(bool isSuccess)
        {
            if (isSuccess)
            {
                SetSuccess();
            }
            else
                SetFailure("");
        }
        public void SetInvalidModel()
        {
            Status = 422;
            Message = "Invalid Model";
        }
        public void AddError(string error, string propertyName)
        {
            if (Errors == null)
            {
                Errors = new List<Error>();
            }
            else
            {
                var errorString = new string[] { error };
                var model = new Error()
                {
                    ErrorMessages = errorString,
                    PropertyName = propertyName
                };
                Errors.Add(model);
            }
        }
        public bool IsSuccess()
        {
            return Status == 200 ? true : false;
        }
    }
    public class ServiceResult : ServiceResult<bool>
    {
        public ServiceResult()
        {
            Status = 404;
            Message = "No Records";
            Data = true;
        }
    }

    public class Error
    {
        public string PropertyName { get; set; }
        public string[] ErrorMessages { get; set; }
    }
}
