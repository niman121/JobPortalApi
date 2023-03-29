using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly RequestDelegate _request;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,
                                   IHttpContextAccessor httpContext,
                                   RequestDelegate request)
        {
            _logger = logger;
            _httpContext = httpContext;
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                
            }
        } 
        public async Task LogExceptionToDb(HttpContext context, Exception ex)
        {
            var exMessageParameter = 0;
        }
    }
}
