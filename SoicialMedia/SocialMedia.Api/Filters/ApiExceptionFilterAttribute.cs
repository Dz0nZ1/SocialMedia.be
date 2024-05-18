using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Domain.Common.Extensions;

namespace SocialMedia.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{

    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            {typeof(FluentValidation.ValidationException), HandleFluentValidationException},
            {typeof(NotFoundException), HandleNotFoundException},
            {typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException},
            {typeof(ArgumentNullException), HandleArgumentNullException},
            {typeof(UserAlreadyExistsException), HandleUserAlreadyExistsException}
            // {typeof(AuthException), HandleAuthException}
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }
    
    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (!_exceptionHandlers.TryGetValue(type,
                out var handler))
            return;

        handler.Invoke(context);
    }
    

    private void HandleFluentValidationException(ExceptionContext context)
    {
        var exception = (FluentValidation.ValidationException)context.Exception;

        var failures = exception.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToArray());

        var details = new ValidationProblemDetails(failures)
        {
            Type = "https://tools.ietf.org/html/rfc7321#section-6.5.1"
        };
        
        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
    private void HandleUserAlreadyExistsException(ExceptionContext context)
    {
        var exception = (UserAlreadyExistsException)context.Exception;

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            Title = "The specified user already exists.",
            Detail = exception.Message
        };

        context.Result = new ConflictObjectResult(details);

        context.ExceptionHandled = true;
    }


    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;
        
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message
        };
        
        context.Result = new NotFoundObjectResult(details);

        context.ExceptionHandled = true;
    }


    // private void HandleAuthException(ExceptionContext context)
    // {
    //
    //     var exception = (AuthException)context.Exception;
    //     
    //     var details = new ProblemDetails
    //     {
    //         Status = StatusCodes.Status400BadRequest,
    //         Title = "Invalid authentication or authorization",
    //         Detail = exception.Message,
    //         Instance = context.HttpContext.Request.Path,
    //     };
    //     
    //     if (exception.AdditionalData != null)
    //     {
    //         details.Extensions["Errors"] = exception.AdditionalData;
    //     }
    //     
    //     context.Result = new BadRequestObjectResult(details);
    //
    //     context.ExceptionHandled = true;
    //
    // }


    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };

        context.ExceptionHandled = true;
    }


    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var details = new ValidationProblemDetails((IDictionary<string, string[]>)exception.AdditionalData!)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleArgumentNullException(ExceptionContext context)
    {
        var exception = (ArgumentNullException)context.Exception;
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Argument Null Exception",
            Detail = exception.Message,
            Instance = context.HttpContext.Request.Path
        };
        
        details.Extensions.Add("ArgumentName", exception.ParamName);
        
        context.HttpContext.Response.Clear();
        context.HttpContext.Response.ContentType = "application/problem+json";
        
        
        using (var writer = new StreamWriter(context.HttpContext.Response.Body))
        {
            (writer, details).Serialize(SerializerExtensions.SettingsWebOptions);
            writer.Flush();
        }
        
        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
    
}