using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess; // Para TarjetasDbContext
using PayTollApp.SharedServices; // Para TarjetaService

var builder = WebApplication.CreateBuilder(args);

// Registrar el TarjetasDbContext
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el TarjetaService
builder.Services.AddScoped<TarjetaService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500") // Cambiar según el frontend
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Agregar controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("AllowFrontend");

// Redirección HTTPS (mantén o elimina dependiendo del entorno)
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
