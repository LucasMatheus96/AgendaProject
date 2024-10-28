using AgendaApi.Data;
using AgendaApi.Repositories;
using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer("Server=localhost;Database=AgendaDb;Trusted_Connection=True;"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Rotas da API CRUD

// GET - Listar todos os eventos
app.MapGet("/eventos", () => "Hello world");

// GET - Buscar um evento por ID
app.MapGet("/eventos/{id}", async (int id, AgendaContext db) =>
    await db.Eventos.FindAsync(id) is Evento evento
        ? Results.Ok(evento)
        : Results.NotFound());

// POST - Criar um novo evento
app.MapPost("/eventos", async (Evento evento, AgendaContext db) =>
{
    db.Eventos.Add(evento);
    await db.SaveChangesAsync();
    return Results.Created($"/eventos/{evento.Id}", evento);
});

// PUT - Atualizar um evento
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

// DELETE - Remover um evento
app.MapDelete("/eventos/{id}", async (int id, AgendaContext db) =>
{
    var evento = await db.Eventos.FindAsync(id);
    if (evento is null) return Results.NotFound();

    db.Eventos.Remove(evento);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Para acessar na raiz
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
