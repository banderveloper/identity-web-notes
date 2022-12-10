using System.Reflection;
using IdentityWebNotes.Application;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Application.Interfaces;
using IdentityWebNotes.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Injection of application and persistence layer groups
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

// Injection CORS (Cross-Origin Resource Sharing), for access from any client
builder.Services.AddCors(options =>
{
    // Allow any client, source and resource (for simplify)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

// Injecting automapper configuration for automapping through IMapWith<>
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
});

// Getting NotesDbContext from services, add pass it to DbInitializer
try
{
    var scope = builder.Services.BuildServiceProvider().CreateScope();
    var context = scope.ServiceProvider.GetService<NotesDbContext>();
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    // todo
}



var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAny");

app.MapControllers();

app.Run();