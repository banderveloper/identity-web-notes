using IdentityServer4.Models;
using IdentityWebNotes.Identity;

var builder = WebApplication.CreateBuilder();

builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();


var app = builder.Build();

app.UseIdentityServer();

app.Run();