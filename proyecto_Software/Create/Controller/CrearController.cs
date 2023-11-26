using Create.DBContext;
using Create.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Create.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrearController : ControllerBase
    {
        private readonly PersonaDbContext _context;
        public CrearController(PersonaDbContext context)
        {
            _context = context;
        }
        [HttpGet("{num_doc}", Name = "GetLog")]
        public async Task<ActionResult<Persona>> GetPersona(string num_doc)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            return await _context.Personas.FindAsync(num_doc);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        // POST: api/Crear
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona([FromForm] Persona persona)
        {
            try
            {
                await _context.Personas.AddAsync(persona);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPersona), new { id = persona.Num_Doc }, persona);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
