using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides;
using PayTollCardApi.Infrastructure.Persistence;
using PayTollCardApi.Core.Interfaces;
using PayTollCardApi.Core.Services;
using System;

var builder = WebApplication.CreateBuilder(args);
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? builder.Configuration["SUPABASE_DB_CONNECTION"]
    ?? throw new InvalidOperationException("Database connection string 'DefaultConnection' is not configured.");
var usuariosConnection = builder.Configuration.GetConnectionString("UsuariosDB") ?? defaultConnection;

// Configuración de DbContext
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseNpgsql(
        defaultConnection,
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseNpgsql(
        usuariosConnection,
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddDbContext<VehiculosDbContext>(options =>
    options.UseNpgsql(
        defaultConnection,
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()
    )
);

// Inyección de Dependencias: Capa de Servicios
builder.Services.AddScoped<TarjetaService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();
builder.Services.AddSingleton(new SqlService(defaultConnection));

builder.Services.AddControllers();

// Configuración de CORS para Portfolio y Entornos Cloud
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHerokuApp", policy =>
    {
        policy.WithOrigins("https://paytollcard-2b6b0c89816c.herokuapp.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "PayTollCard API - Sistema de Telepeaje", 
        Version = "v1",
        Description = "API RESTful orientada a Servicios para gestión de tarjetas de telepeaje."
    });
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayTollCard API v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var isHeroku = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORT"));
if (!isHeroku)
{
    app.UseHttpsRedirection();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowHerokuApp");

app.MapControllers();
app.Run();
