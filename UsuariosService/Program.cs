using Microsoft.EntityFrameworkCore;
using UsuariosService.Data;
using Microsoft.OpenApi.Models; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Usuarios API", Version = "v1" });
});

builder.Services.AddDbContext<UsuariosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsuariosDB")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuarios API v1");
        c.RoutePrefix = string.Empty; 
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
