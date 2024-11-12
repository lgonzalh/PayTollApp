using Microsoft.EntityFrameworkCore;
using VehiculosService.Data;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la base de datos para VehiculosDbContext
builder.Services.AddDbContext<VehiculosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar controladores de Veh�culos
builder.Services.AddControllers();

// Configuraci�n de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500") // Ajusta seg�n la URL del frontend
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Swagger para VehiculosService
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehiculosService API", Version = "v1" });

    // Limitar Swagger solo a controladores de Vehiculos
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.ActionDescriptor.RouteValues["controller"].StartsWith("Vehiculos");
    });
});


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
