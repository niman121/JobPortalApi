using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service;
using JobPortal.Utility.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Utility
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context, JobDbContext dbContext)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToDbAsync(context, ex,dbContext);
                await HandleExceptionAsync(context, ex);
            }
        } 
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType= "application/json";
            var exceptionData = GetExceptionDetail(ex);
            context.Response.StatusCode = (int)exceptionData.Status;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(exceptionData));
        }

        private ServiceResult<string> GetExceptionDetail(Exception ex)
        {
            var model = GetErrorResponse(ex);

            var execptionType = ex.GetType();
            var value = GetValueByExceptionType(execptionType);

            switch (value)
            {
                case 1:
                case 4:
                    model.Status = (int)HttpStatusCode.PreconditionFailed;
                    break;
                case 2:
                    model.Status = (int)HttpStatusCode.NotFound;
                    break;
                case 3:
                    model.Status = (int)HttpStatusCode.BadRequest;
                    break;
                case 5:
                default:
                    model.Status = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            return model;
        }
        private async Task LogExceptionToDbAsync(HttpContext context, Exception ex, JobDbContext _context)
        {
            var execptionLog = new ExceptionLog()
            {
                Message = ex.Message,
                Type = ex.GetType().ToString(),
                Source = ex.StackTrace,
                Url = context.Request?.Path.Value
            };
            await _context.AddAsync(execptionLog);
            await _context.SaveChangesAsync();
        }

        private ServiceResult<string> GetErrorResponse(Exception ex)
        {
            var result = new ServiceResult<string>();
            var errorMessage = new string[1];
            errorMessage[0] = ex.StackTrace.ToString();
            var errorList = new List<Error>
            {
                new Error
                {
                    PropertyName = ex.GetType().ToString(),
                    ErrorMessages= errorMessage
                }
            };
            result.Errors = errorList;
            result.Message = ex.Message;
            return result;
        }

        private int GetValueByExceptionType(Type exceptionType)
        {
            var errorCode = new Dictionary<Type, int>()
            {
                { typeof(DuplicateRecordException),1},
                { typeof(RecordNotFoundException), 2},
                { typeof(BadRequestException), 3},
                { typeof(ValidationException), 4},
                { typeof(NullReferenceException), 5}
            };
            
            return errorCode.Where(q => q.Key == exceptionType).Select(q => q.Value).FirstOrDefault();
        }
    }
}
