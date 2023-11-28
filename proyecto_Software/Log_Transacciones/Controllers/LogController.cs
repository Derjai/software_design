using Log_Transacciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Log_Transacciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IMongoCollection<Log> _logCollection;

        public LogController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var dataBase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _logCollection = dataBase.GetCollection<Log>("log_transacciones");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            try 
            { 
                var logs = await _logCollection.Find(Builders<Log>.Filter.Empty).ToListAsync();
                if (logs == null) return NotFound("No existen registros en la base de datos");
                return Ok(logs); 
            } 
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/api/Log/doc/{num_doc}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByDoc(string num_doc)
        {
            try
            {
                var filterDefinition = Builders<Log>.Filter.Eq(x => x.Num_Doc, num_doc);
                var logs = await _logCollection.Find(filterDefinition).ToListAsync();
                if (logs == null) return NotFound($"No hay registros de transacciones con el numero documento {num_doc}");
                return Ok(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/api/Log/fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByDate(DateTime fecha)
        {
            try
            {
                var filterDefinition = Builders<Log>.Filter.Eq(x => x.Fecha, fecha);
                var logs = await _logCollection.Find(filterDefinition).ToListAsync();
                if (logs == null) return NotFound($"No hay registros de transacciones en la fecha: {fecha}");
                return Ok(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/api/Log/fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<Log>>> GetByType(string tipo)
        {
            try
            {
                var filterDefinition = Builders<Log>.Filter.Eq(x => x.Tipo_Transaccion, tipo);
                var logs = await _logCollection.Find(filterDefinition).ToListAsync();
                if (logs == null) return NotFound($"No hay registros de transacciones de tipo {tipo}");
                return Ok(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}", Name = "GetLog")]
        public async Task<ActionResult<Log>> GetLog(string id)
        {
            var filterDefinition = Builders<Log>.Filter.Eq(x => x.Id_Log, id);
            return await _logCollection.Find(filterDefinition).FirstOrDefaultAsync();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<Log>> CreateLog([FromBody] Log log)
        {
            try
            {
                await _logCollection.InsertOneAsync(log);
                return CreatedAtAction(nameof(GetLog), new { id = log.Id_Log }, log);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar en MongoDB: {ex}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
