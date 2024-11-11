using AgendaApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApi.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<Contato>> GetContatosAsync();
        Task<Contato> GetContatoByIdAsync(int id);
        Task<Contato> CreateContatoAsync(Contato contato);
        Task<bool> UpdateContatoAsync(int id, Contato contatoAtualizado);
        Task<bool> DeleteContatoAsync(int id);
    }
}
