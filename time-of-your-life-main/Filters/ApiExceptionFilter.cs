namespace time_of_your_life.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using time_of_your_life.Infrastructure.ExceptionHandler;
    using time_of_your_life.Infrastructure.Transport.Core.Response;

    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            BaseResponse baseResponse = null;

            if (context.Exception is DomainException)
            {
                var ex = context.Exception as DomainException;
                context.Exception = null;

                baseResponse = new BaseResponse();
                baseResponse.State.HasError = true;
                baseResponse.State.TipoError = nameof(DomainException);
                baseResponse.State.MensajeError = ex.Message;
                baseResponse.State.MensajeDetalle = $"{ex.InnerException?.Message} --- {ex.StackTrace}";

                context.ExceptionHandled = true;

                context.Result = new JsonResult(baseResponse);
            }

            base.OnException(context);
        }
    }
}
