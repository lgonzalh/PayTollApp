using ShortcutService.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar SqlService
builder.Services.AddScoped<SqlService>();

// **No agregues configuraci�n de autenticaci�n y autorizaci�n si no la est�s utilizando**

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// **Elimina o comenta el middleware de autenticaci�n y autorizaci�n**
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
