using AgendaApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaApi.Services;

namespace AgendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IContatoService _contatoService;

        public AgendaController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            var contatos = await _contatoService.GetContatosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _contatoService.GetContatoByIdAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(Contato contato)
        {
            var novoContato = await _contatoService.CreateContatoAsync(contato);
            return CreatedAtAction(nameof(GetContato), new { id = novoContato.Id }, novoContato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, Contato contatoAtualizado)
        {
            var sucesso = await _contatoService.UpdateContatoAsync(id, contatoAtualizado);
            if (!sucesso)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var sucesso = await _contatoService.DeleteContatoAsync(id);
            if (!sucesso)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
