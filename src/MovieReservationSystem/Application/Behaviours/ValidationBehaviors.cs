using FluentValidation;
using MediatR;

namespace MovieReservationSystem.Application.Behaviours;

public class ValidationBehaviors<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Validating {typeof(TRequest).Name}"); // Debug output
        
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
        
            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
    
        return await next();
    }
}