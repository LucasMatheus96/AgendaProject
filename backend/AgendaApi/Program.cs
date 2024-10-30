using AgendaApi.Data;
using AgendaApi.Repositories;
using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:5174")  // Substituir pela URL correta do frontend
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Configuração do Entity Framework
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer("Server=localhost;Database=AgendaDb;Trusted_Connection=True;"));

// Adicionando serviços para controladores e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitando Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

// ATENÇÃO: Middleware de CORS deve vir antes das rotas
app.UseCors("AllowFrontend");

// Redirecionamento HTTPS
app.UseHttpsRedirection();

// Middleware de autorização
app.UseAuthorization();

// Mapeando controladores
app.MapControllers();

// Rotas da API CRUD
app.MapGet("/eventos/agenda", () => 
{
    var eventos = new List<Evento>();
    
    Evento evento = new Evento();
    evento.Id = 1;
    evento.Data= new DateTime();
    evento.Descricao = "Validando Lucas";
    evento.Titulo = "Teste De Conehcimento";

    eventos.Add(evento);

    return Results.Ok(eventos);

});

app.MapGet("/eventos/{id}", async (int id, AgendaContext db) =>
    await db.Eventos.FindAsync(id) is Evento evento
        ? Results.Ok(evento)
        : Results.NotFound());

app.MapPost("/eventos", async (Evento evento, AgendaContext db) =>
{
    db.Eventos.Add(evento);
    await db.SaveChangesAsync();
    return Results.Created($"/eventos/{evento.Id}", evento);
});

app.MapPut("/eventos/{id}", async (int id, Evento eventoAtualizado, AgendaContext db) =>
{
    var evento = await db.Eventos.FindAsync(id);
    if (evento is null) return Results.NotFound();

    evento.Titulo = eventoAtualizado.Titulo;
    evento.Data = eventoAtualizado.Data;
    evento.Descricao = eventoAtualizado.Descricao;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/eventos/{id}", async (int id, AgendaContext db) =>
{
    var evento = await db.Eventos.FindAsync(id);
    if (evento is null) return Results.NotFound();

    db.Eventos.Remove(evento);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Executar a aplicação
app.Run();

