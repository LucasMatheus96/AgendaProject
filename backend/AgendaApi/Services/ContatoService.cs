using AgendaApi.Data;
using AgendaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApi.Services
{
	public class ContatoService : IContatoService
	{
		private readonly AgendaContext _context;

		public ContatoService(AgendaContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Contato>> GetContatosAsync()
		{
			return await _context.Contatos.ToListAsync();
		}

		public async Task<Contato> GetContatoByIdAsync(int id)
		{
			return await _context.Contatos.FindAsync(id);
		}

		public async Task<Contato> CreateContatoAsync(Contato contato)
		{
			_context.Contatos.Add(contato);
			await _context.SaveChangesAsync();
			return contato;
		}

		public async Task<bool> UpdateContatoAsync(int id, Contato contatoAtualizado)
		{
			var contato = await _context.Contatos.FindAsync(id);
			if (contato == null) return false;

			contato.Nome = contatoAtualizado.Nome;
			contato.dtDataCadastro = contatoAtualizado.dtDataCadastro;
			contato.Telefone = contatoAtualizado.Telefone;
			contato.Email = contatoAtualizado.Email;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteContatoAsync(int id)
		{
			var contato = await _context.Contatos.FindAsync(id);
			if (contato == null) return false;

			_context.Contatos.Remove(contato);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
