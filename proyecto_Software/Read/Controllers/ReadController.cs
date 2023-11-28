using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Read.DBContext;
using Read.Models;

namespace Read.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        private readonly PersonaDbContext _context;
        public ReadController(PersonaDbContext context)
        {
            _context = context;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
        {
            try
            {
                var personas = await _context.Personas.ToListAsync();
                if (personas.Count == 0) return NotFound("No existen registros en la base de datos");
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/api/Read/doc/{doc}")]
        public async Task<ActionResult<Persona>> GetById(string doc)
        {
            try
            {
                var persona = await _context.Personas.FirstOrDefaultAsync(x => x.Num_Doc == doc);
                if (persona == null) return NotFound($"No existe el registro con el número de documento {doc}");
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
