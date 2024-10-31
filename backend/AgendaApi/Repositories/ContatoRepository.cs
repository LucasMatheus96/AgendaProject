using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;
using AgendaApi.Data;
using System;

namespace AgendaApi.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly AgendaContext _context;

    public ContatoRepository(AgendaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contato>> GetAllAsync() =>
        await _context.Contatos.ToListAsync();

    public async Task<Contato?> GetByIdAsync(int id) =>
        await _context.Contatos.FindAsync(id);

    public async Task AddAsync(Contato contato)
    {
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contato contato)
    {
        _context.Contatos.Update(contato);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contato contato)
    {
        _context.Contatos.Remove(contato);
        await _context.SaveChangesAsync();
    }
}