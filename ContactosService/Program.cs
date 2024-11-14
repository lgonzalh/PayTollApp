using Microsoft.EntityFrameworkCore;
using PayTollApp.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Agregar el contexto de la base de datos
builder.Services.AddDbContext<TarjetasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar controladores
builder.Services.AddControllers();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500") // Dirección del frontend
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactosService API V1");
        options.RoutePrefix = "swagger";
    });
}

// Middleware para redireccionar CORS y autorizaciones
app.UseCors("AllowFrontend");

// Descomenta esta línea si necesitas redirección a HTTPS
// app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
