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

// Habilitar Swagger para documentaci�n
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configurar el middleware de excepci�n y HSTS para producci�n
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Redireccionar HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors("AllowAll");

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar archivos est�ticos desde wwwroot
app.UseStaticFiles();

// Configurar el enrutamiento
app.UseRouting();

// Habilitar autorizaci�n (si aplica)
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Manejar rutas para el frontend (SPA)
app.MapFallbackToFile("index.html");

app.Run();
