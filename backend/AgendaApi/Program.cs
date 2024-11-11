using AgendaApi.Data;
using AgendaApi.Repositories;
using AgendaApi.Models;

using Microsoft.EntityFrameworkCore;
using AgendaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5174")  // Substituir pela URL correta do frontend
              .AllowAnyMethod()
              .AllowAnyHeader());
});


builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer("Server=localhost;Database=AgendaDb;Trusted_Connection=True;TrustServerCertificate=True;"));


builder.Services.AddControllers();
builder.Services.AddScoped<IContatoService, ContatoService>(); // Registro da interface e implementação do serviço
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}


app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

