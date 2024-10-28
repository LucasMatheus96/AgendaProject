namespace AgendaApi.DTOs;

public class EventoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}