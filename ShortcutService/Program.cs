using ShortcutService.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar SqlService
builder.Services.AddScoped<SqlService>();

// **No agregues configuración de autenticación y autorización si no la estás utilizando**

// Agrega este código para configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://127.0.0.1:5500")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Agrega UseCors antes de UseAuthorization y UseEndpoints
app.UseCors("AllowLocalhost");


// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **Elimina o comenta el middleware de autenticación y autorización**
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
