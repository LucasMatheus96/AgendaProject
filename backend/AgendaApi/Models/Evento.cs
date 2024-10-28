namespace AgendaApi.Models;

public class Evento{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;

    public DateTime Data { get; set; }

    public string? Descricao { get; set; }
}