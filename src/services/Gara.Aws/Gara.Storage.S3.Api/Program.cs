using Amazon.S3;
using Gara.Exceptions.Filters;
using Gara.Storage.S3.Application.Repositories;
using Gara.Storage.S3.Domain.Repositories;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
    options.Filters.Add(typeof(ValidateModelFilter));
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
});

builder.Services.AddAWSService<IAmazonS3>(configuration.GetAWSOptions());
builder.Services.AddScoped<IBucketsRepository, BucketsRepository>();
builder.Services.AddScoped<IFilesRepository, FilesRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Validation Service API",
        Description = "Supporting apis: address search/detail, phone validation, email validation, bank validation "
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Gara")).ToArray();
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(currentAssemblies));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Validation Service API V1"); });
}

app.MapGet("/", () => "Hello World!");

app.Run();
