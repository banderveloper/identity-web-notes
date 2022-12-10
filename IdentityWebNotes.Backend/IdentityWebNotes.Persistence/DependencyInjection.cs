using IdentityWebNotes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityWebNotes.Persistence;

public static class DependencyInjection
{
    // Extension method for service collection, adding everything needed from persistence layer
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Injecting db context
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<NotesDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        // Injecting scope for dbContext, implementation gets from service because...
        // ...it is already injected before (AddDbContext)
        // AddScoped<INotesDbContext, NotesDbContext> will create second instance of NotesDbContext
        services.AddScoped<INotesDbContext>(provider =>
            provider.GetService<NotesDbContext>());
        
        return services;
    }
}