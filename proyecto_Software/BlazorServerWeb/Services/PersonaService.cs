using BlazorServerWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerWeb.Services
{
    public class PersonaService
    {
        private readonly HttpClient _httpClient;

        public PersonaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Persona>> GetPersonasAsync()
        {
            try 
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Persona>>("/api/Read");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task<Persona> GetPersonaAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Persona>($"/api/Read/doc/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task<Persona> AddPersonaAsync(Persona persona)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<Persona>("/api/Create", persona);
                return await response.Content.ReadFromJsonAsync<Persona>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task UpdatePersonaAsync(Persona persona)
        {
            try
            {
                await _httpClient.PutAsJsonAsync<Persona>($"/api/Update/{persona.Num_Doc}", persona);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task DeletePersonaAsync(int id)
        {
            try
            {
                await _httpClient.DeleteAsync($"/api/Delete/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }
    }
}
