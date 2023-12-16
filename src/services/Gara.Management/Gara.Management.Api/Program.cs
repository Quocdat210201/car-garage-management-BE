using Gara.Management.Api.Extensions;
using Gara.Management.Application.Data;
using Gara.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gara.Management.Application.Services.DataInitialize;
using Microsoft.EntityFrameworkCore;
using Gara.Cache.Redis.Extensions;
using Gara.Management.Application.Storages;
using Gara.Management.Domain.Storages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Gara.Exceptions.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var assembly = Assembly.Load("Gara.Management.Domain");

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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
builder.Services.AddDbContext(configuration);
builder.Services.AddCors();

builder.Services.AddRedisCache(configuration);
builder.Services.AddScoped<IGaraStorage, GaraStorage>();

builder.Services.AddIdentity<GaraApplicationUser, GaraApplicationRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<GaraManagementDBContent>().AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:ThirdPartyRelationshipSecret"))),
            ValidAudience = configuration.GetValue<string>("AppSettings:TokenAudience"),
            ValidIssuer = configuration.GetValue<string>("AppSettings:TokenIssuer"),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });

// Register the Swagger generator, defining 1 or more Swagger documents
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
builder.Services.AddRepositories();

var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("Gara")).ToArray();
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(currentAssemblies));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IDataInitializeService, RoleDataInitializeService>();
builder.Services.AddTransient<IDataInitializeService, UserDataInitializeService>();

var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Validation Service API V1"); });
}
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

app.MapGet("/", () => "Hello World!");


var db = serviceProvider.GetRequiredService<GaraManagementDBContent>();

// Auto run migrate
db.Database.MigrateAsync().Wait();

// Get the service  
var dataInitializeServices = serviceProvider.GetServices<IDataInitializeService>();
foreach (var service in dataInitializeServices)
{
    //service.RunAsync();
}

app.Run();