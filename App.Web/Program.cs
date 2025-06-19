using App.Application.Interfaces;
using App.Application.Mappings;
using App.Core.Interfaces;
using App.Infrastructure.Data;
using App.Infrastructure.DependencyInjection;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRespository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//Source :https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/docs/configure-and-customize-swaggergen.md#add-security-definitions-and-requirements
builder.Services.AddSwaggerGen(swagger => {
    swagger.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "ASP.NET 8 Web API",
        Description = "Authentication with JWT"
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173") // React dev server
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


//Calls the InfrastructureServices Method at Infrastructure/DependencyInjection/ServiceContainer.cs
builder.Services.InfrastructureServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
