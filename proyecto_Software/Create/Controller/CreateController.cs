using Create.DBContext;
using Create.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Create.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateController : ControllerBase
    {
        private readonly PersonaDbContext _context;
        public CreateController(PersonaDbContext context)
        {
            _context = context;
        }
        [HttpGet("{num_doc}", Name = "GetPersona")]
        public async Task<ActionResult<Persona>> GetPersona(string num_doc)
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            return await _context.Personas.FindAsync(num_doc);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        // POST: api/Create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona([FromBody] Persona persona)
        {
            try
            {
                await _context.Personas.AddAsync(persona);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPersona), new { num_doc = persona.Num_Doc }, persona);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
