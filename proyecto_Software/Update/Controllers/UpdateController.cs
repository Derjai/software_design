using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Update.DBContext;
using Update.Models;

namespace Update.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly PersonaDbContext _context;
        public UpdateController(PersonaDbContext context)
        {
            _context = context;
        }
        // POST: api/Update
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("/api/Update/{num_doc}")]
        public async Task<ActionResult<Persona>> PostPersona(string num_doc,[FromForm] Persona per)
        {
            try
            {
                var persona = await _context.Personas.FindAsync(num_doc);
                if (persona == null)
                {
                    return NotFound("No se encontró el registro");
                }
                _context.Entry(persona).State = EntityState.Detached;
                persona = per;
                _context.Personas.Update(persona);
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
