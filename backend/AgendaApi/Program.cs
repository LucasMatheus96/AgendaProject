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
    var lstContato = new List<Contato>();
    
    Contato contato = new Contato();
    contato.Id = 1;
    contato.Data= new DateTime();
    contato.Telefone = "Validando Lucas";
    contato.Email = "Teste De Conehcimento";

    lstContato.Add(contato);

    return Results.Ok(lstContato);

});

app.MapGet("/eventos/{id}", async (int id, AgendaContext db) =>
    await db.Contatos.FindAsync(id) is Contato contato
        ? Results.Ok(contato)
        : Results.NotFound());

app.MapPost("/eventos", async (Contato contato, AgendaContext db) =>
{
    db.Contatos.Add(contato);
    await db.SaveChangesAsync();
    return Results.Created($"/eventos/{contato.Id}", contato);
});

app.MapPut("/eventos/{id}", async (int id, Contato contatoAtualizado, AgendaContext db) =>
{
    var contato = await db.Contatos.FindAsync(id);
    if (contato is null) return Results.NotFound();

    contato.Nome = contatoAtualizado.Nome;
    contato.Data = contatoAtualizado.Data;
    contato.Telefone = contatoAtualizado.Telefone;
    contato.Email = contatoAtualizado.Email;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/eventos/{id}", async (int id, AgendaContext db) =>
{
    var evento = await db.Contatos.FindAsync(id);
    if (evento is null) return Results.NotFound();

    db.Contatos.Remove(evento);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Executar a aplicação
app.Run();

