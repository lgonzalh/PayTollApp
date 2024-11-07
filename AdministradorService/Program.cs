using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;
using AdministradorService.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar el contexto de la base de datos
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios
builder.Services.AddScoped<IAdministradorService, AdministradorService.Services.AdministradorService>();

// Agregar controladores
builder.Services.AddControllers();

// Habilitar Swagger para documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
