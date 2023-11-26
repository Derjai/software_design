using Delete.DBContext;
using Delete.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly PersonaDbContext _context;
        public DeleteController(PersonaDbContext context)
        {
            _context = context;
        }
        // POST: api/Update
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("/api/Delete/{num_doc}")]
        public async Task<ActionResult<Persona>> PostPersona(string num_doc)
        {
            try
            {
                var persona = await _context.Personas.FindAsync(num_doc);
                if (persona == null)
                {
                    return NotFound("No se encontró el registro");
                }
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
