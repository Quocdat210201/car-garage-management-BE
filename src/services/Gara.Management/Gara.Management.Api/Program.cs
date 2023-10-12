using Gara.Management.Api.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var assembly = Assembly.Load("Gara.Management.Domain");

builder.Services.AddDbContext(configuration);

builder.Services.AddControllers();
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Validation Service API",
        Description = "Supporting apis: address search/detail, phone validation, email validation, bank validation "
    });
});
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Validation Service API V1"); });

app.MapGet("/", () => "Hello World!");

app.Run();
