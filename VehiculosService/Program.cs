using Microsoft.EntityFrameworkCore;
using VehiculosService.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vehiculos API", Version = "v1" });
});

// Registrar el DbContext con la base de datos
builder.Services.AddDbContext<VehiculosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehiculosDB")));

var app = builder.Build();

// Configurar el middleware de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehiculos API v1");
        c.RoutePrefix = string.Empty; // Esto hará que Swagger esté en la raíz
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
