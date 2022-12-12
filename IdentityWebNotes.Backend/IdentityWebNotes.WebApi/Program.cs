using System.Reflection;
using IdentityWebNotes.Application;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Application.Interfaces;
using IdentityWebNotes.Persistence;
using IdentityWebNotes.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Injection of application and persistence layer groups
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();

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

builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;
        config.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001/";
        options.Audience = "notesapi";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddSwaggerGen();

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

app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    // get to swagger UI using root uri
    config.RoutePrefix = string.Empty;
    
    config.SwaggerEndpoint("swagger/v1/swagger.json", "IdentityNotes API");
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();