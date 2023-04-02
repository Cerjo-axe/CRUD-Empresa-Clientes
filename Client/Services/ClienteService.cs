using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DTO;

namespace Client.Services;

public class ClienteService : IClienteService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ClienteService> _logger;

    public ClienteService(HttpClient httpClient, ILogger<ClienteService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    public async Task AddCliente(ClienteDTO obj)
    {
        try
        {
            var cliente = JsonSerializer.Serialize(obj);
            StringContent content = new StringContent(cliente,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/Clientes",content);
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao adicionar cliente");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task DeletarCliente(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Clientes/{id}");
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar cliente");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<ClienteDTO> GetCliente(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Clientes/{id}");
            if(response.IsSuccessStatusCode)
            {
                if(response.StatusCode==System.Net.HttpStatusCode.NoContent)
                {
                    return default(ClienteDTO);
                }
                return await response.Content.ReadFromJsonAsync<ClienteDTO>();
            }
            else{
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro ao obter cliente de id {id}");
                throw new Exception($"status code: {response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            _logger.LogError("Erro ao acessar o cliente");
            throw;
        }
    }

    public async Task<IEnumerable<ClienteDTO>> GetClientes()
    {
        try
        {
            var clientes = await _httpClient.GetFromJsonAsync<IEnumerable<ClienteDTO>>("api/Clientes");
            return clientes;
        }
        catch (System.Exception)
        {
            _logger.LogError("Erro ao acessar clientes da api");
            throw;
        }
    }

    public async Task UpdateCliente(ClienteDTO obj)
    {
        try
        {
            var cliente = JsonSerializer.Serialize(obj);
            StringContent content = new StringContent(cliente,Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync("api/Clientes",content);
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar cliente");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
