using BlazorServerWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
                return await _httpClient.GetFromJsonAsync<IEnumerable<Persona>>("http://host.docker.internal:8001/api/Read");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task<Persona> GetPersonaAsync(string id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Persona>($"http://host.docker.internal:8001/api/Read/doc/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task AddPersonaAsync(Persona persona)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<Persona>("http://host.docker.internal:8001/api/Create", persona);
                response.EnsureSuccessStatusCode();
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
                var response =await _httpClient.PutAsJsonAsync<Persona>($"http://host.docker.internal:8001/api/Update/{persona.Num_Doc}", persona);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }

        public async Task DeletePersonaAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"http://host.docker.internal:8001/api/Delete/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los registros: {ex.Message}");
                throw;
            }
        }
    }
}
