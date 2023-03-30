using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service;
using JobPortal.Utility.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RequestDelegate _request;
        private readonly JobDbContext _context;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,
                                   IHttpContextAccessor httpContext,
                                   RequestDelegate request,
                                   JobDbContext context)
        {
            _logger = logger;
            _httpContext = httpContext;
            _request = request;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToDbAsync(context, ex);
            }
        } 
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType= "application/json";
            var exceptionData = GetExceptionDetail(ex);
        }

        private async Task<ServiceResult<string>> GetExceptionDetail(Exception ex)
        {
            var model = await GetErrorResponseAsync(ex);

            var execptionType = ex.GetType();
            var value = await GetValueByExceptionType(execptionType);

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
        private async Task LogExceptionToDbAsync(HttpContext context, Exception ex)
        {
            //var exMessageParameter = DataProvider.GetStringSqlParameter("@Message",ex.Message.ToString());
            //var exTypeParameter = DataProvider.GetStringSqlParameter("@Type",ex.GetType().ToString());
            //var exSourceParameter = DataProvider.GetStringSqlParameter("@Source",ex.StackTrace.ToString());
            //var exUrlParameter = DataProvider.GetStringSqlParameter("@Url",context.Request?.Path.Value.ToString());

            //var sqlParameters = new List<SqlParameter>()
            //{
            //    exMessageParameter, exTypeParameter, exSourceParameter, exUrlParameter
            //};

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

        private async Task<ServiceResult<string>> GetErrorResponseAsync(Exception ex)
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

        private async Task<int> GetValueByExceptionType(Type exceptionType)
        {
            var errorCode = new Dictionary<Type, int>()
            {
                { typeof(DuplicateRecordException),1},
                { typeof(RecordNotFoundException), 2},
                {typeof(BadRequestException), 3},
                { typeof(ValidationException), 4},
                { typeof(NullReferenceException), 5}
            };

            return errorCode.Where(q => q.Key == exceptionType).Select(q => q.Value).FirstOrDefault();
        }
    }
}
