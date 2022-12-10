using System.Reflection;
using FluentValidation;
using IdentityWebNotes.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityWebNotes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}