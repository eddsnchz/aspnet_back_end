using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BACKEND.Filters
{
    public class MyGlobalExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<MyGlobalExceptionFilter> logger;

        public MyGlobalExceptionFilter(ILogger<MyGlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}
