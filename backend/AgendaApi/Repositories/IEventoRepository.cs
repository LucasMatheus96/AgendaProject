using AgendaApi.Models;

namespace AgendaApi.Repositories;

public interface IEventoRepository
{
    Task<IEnumerable<Evento>> GetAllAsync();
    Task<Evento?> GetByIdAsync(int id);
    Task AddAsync(Evento evento);
    Task UpdateAsync(Evento evento);
    Task DeleteAsync(Evento evento);
}