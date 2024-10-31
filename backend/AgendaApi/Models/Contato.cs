using System;

namespace AgendaApi.Models;

public class Contato
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public DateTime? dtDataCadastro { get; set; }

    public string? Telefone { get; set; }
    public string? Email { get; set; }
}