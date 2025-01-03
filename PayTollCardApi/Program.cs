using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PayTollCardApi.Data;
using PayTollCardApi.DataAccess;
using PayTollCardApi.Services;
using PayTollCardApi.SharedServices;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Configuración de DbContext con EnableRetryOnFailure
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("UsuariosDB"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddDbContext<VehiculosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
    )
);

// Registro de Servicios
builder.Services.AddScoped<TarjetaService>();
builder.Services.AddScoped<IAdministradorService, AdministradorService>();

// **Agregar esta línea para registrar SqlService**
builder.Services.AddSingleton<SqlService>(new SqlService(builder.Configuration.GetConnectionString("DefaultConnection")));

// Añadir Controllers
builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowHerokuApp", policy =>
    {
        policy.WithOrigins("https://paytollcard-2b6b0c89816c.herokuapp.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayTollCard API", Version = "v1" });
});

var app = builder.Build();

// Configurar Forwarded Headers para Heroku
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configuración de Middleware
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

// Desactivar Redirección HTTPS en Heroku
var isHeroku = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PORT"));
if (!isHeroku)
{
    app.UseHttpsRedirection();
}

// Servir archivos estáticos y archivos por defecto
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

// Aplicar CORS
app.UseCors("AllowHerokuApp");

// Opcional: Autenticación y Autorización
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();
app.Run();
