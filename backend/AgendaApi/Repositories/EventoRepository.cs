using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;
using AgendaApi.Data;

namespace AgendaApi.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly AgendaContext _context;

    public EventoRepository(AgendaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Evento>> GetAllAsync() =>
        await _context.Eventos.ToListAsync();

    public async Task<Evento?> GetByIdAsync(int id) =>
        await _context.Eventos.FindAsync(id);

    public async Task AddAsync(Evento evento)
    {
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Evento evento)
    {
        _context.Eventos.Update(evento);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Evento evento)
    {
        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
}