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


builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer("Server=localhost;Database=AgendaDb;Trusted_Connection=True;TrustServerCertificate=True;"));


builder.Services.AddControllers();
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

// Rotas da API CRUD
app.MapGet("/api/agenda", async (AgendaContext db) =>
{
    var contatos = await db.Contatos.ToListAsync();
    return Results.Ok(contatos);
});

app.MapGet("/api/agenda/contato/{id}", async (int id, AgendaContext db) =>
    await db.Contatos.FindAsync(id) is Contato contato
        ? Results.Ok(contato)
        : Results.NotFound());

app.MapPost("/api/agenda/contato", async (Contato contato, AgendaContext db) =>
{
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"api/contato/{contato.Id}", contato);
});

app.MapPut("/api/agenda/contato/{id}", async (int id, Contato contatoAtualizado, AgendaContext db) =>
{
    var contato = await db.Contatos.FindAsync(id);
    if (contato is null) return Results.NotFound();

    contato.Nome = contatoAtualizado.Nome;
    contato.dtDataCadastro = contatoAtualizado.dtDataCadastro;
    contato.Telefone = contatoAtualizado.Telefone;
    contato.Email = contatoAtualizado.Email;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/agenda/contato/{id}", async (int id, AgendaContext db) =>
{
    var contato = await db.Contatos.FindAsync(id);
    if (contato is null) return Results.NotFound();

    db.Contatos.Remove(contato);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Executar a aplicação
app.Run();

