using Microsoft.EntityFrameworkCore;
using PayTollApp.SharedServices;
using VehiculosService.Data;
using PayTollApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de VehiculosDbContext
builder.Services.AddDbContext<VehiculosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuraci�n de TarjetasDbContext
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de TarjetaService
builder.Services.AddScoped<TarjetaService>();

// Agregar controladores
builder.Services.AddControllers();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Swagger para documentaci�n de API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
