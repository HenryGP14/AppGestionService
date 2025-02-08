using Api.Extension;
using Api.Extensions;
using Application.Extensions;
using Application.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infraestructure.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var Cors = "CorsPolicy";

// Add services to the container.
builder.Services.ServicesInfraestructure(Configuration);

// Registrar los servicios personalizados directamente
builder.Services.AddSingleton(Configuration);

// Configurar FluentValidation
builder.Services.AddFluentValidationAutoValidation(); // Sirve para validar autometicamente los modelos de las peticiones antes que lleguen al controlador
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic)); // Registrar validadores
builder.Services.AddAuthentication(Configuration); // Se añade injeccion para la autenticacion
builder.Services.AddScoped<ValidateRequestExtension>();
builder.Services.AddScoped<ValidateClaimExtension>();
builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserMappingProfile>();
});
builder.Services.AddTransient<UserApprovalResolver>();

builder.Services.AddControllers();
// Configura la validacion personalizada si es necesario
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: Cors, builder =>
    {
        builder.WithOrigins("*");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

// Configuracion de Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Configura CORS
app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }