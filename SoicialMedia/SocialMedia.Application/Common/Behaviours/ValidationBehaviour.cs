using FluentValidation;
using MediatR;
using SocialMedia.Application.Common.Extensions;
using ValidationException = SocialMedia.Application.Common.Exceptions.ValidationException;

namespace SocialMedia.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any()) return await next();


        var context = new ValidationContext<TRequest>(request);

        var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResult.Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any()) throw new ValidationException(failures.ToGroup());

        return await next();

    }
}