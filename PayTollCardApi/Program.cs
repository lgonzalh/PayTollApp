using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PayTollCardApi.Data;
using PayTollCardApi.DataAccess;
using PayTollCardApi.Services;
using PayTollCardApi.SharedServices;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de DbContext con EnableRetryOnFailure
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

// A�adir Controllers
builder.Services.AddControllers();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

// Configuraci�n de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayTollCard API", Version = "v1" });
});

var app = builder.Build();

// Configuraci�n de Middleware
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

// Configurar el puerto desde Heroku
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");

// Opcional: Autenticaci�n y Autorizaci�n
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();
app.Run();
