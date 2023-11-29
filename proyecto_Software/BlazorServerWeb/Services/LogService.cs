using BlazorServerWeb.Models;

namespace BlazorServerWeb.Services
{
    public class LogService
    {
        private readonly HttpClient _httpClient;

        public LogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<IEnumerable<Log>> GetLogsAsync()
        {
            try
            {

                return await _httpClient.GetFromJsonAsync<IEnumerable<Log>>("http://host.docker.internal:8001/api/Log");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Log>> GetLogAsync(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Log>>($"http://host.docker.internal:8001/api/Log/doc/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
            
        }

        public async Task<IEnumerable<Log>> GetByType(string tipo)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Log>>($"http://host.docker.internal:8001/api/Log/tipo/{tipo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }   
        }

        public async Task<IEnumerable<Log>> GetByDate(DateTime fecha)
        {
            try
            {
                string formattedDate = fecha.ToString("yyyy-MM-dd");
                return await _httpClient.GetFromJsonAsync<IEnumerable<Log>>($"http://host.docker.internal:8001/api/Log/fecha/{formattedDate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task<Log> AddLogAsync(Log log)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<Log>("http://host.docker.internal:8001/api/Log", log);
                return await response.Content.ReadFromJsonAsync<Log>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

    }
}
