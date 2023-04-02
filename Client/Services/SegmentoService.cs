using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using DTO;

namespace Client.Services;

public class SegmentoService : ISegmentoService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SegmentoService> _logger;

    public SegmentoService(HttpClient httpClient, ILogger<SegmentoService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task AddSegmento(SegmentoDTO obj)
    {
        try
        {
            var segmento = JsonSerializer.Serialize(obj);
            StringContent content = new StringContent(segmento,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/Segmentos",content);
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao adicionar segmento");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public async Task UpdateSegmento(SegmentoDTO obj){
        try
        {
            var segmento = JsonSerializer.Serialize(obj);
            StringContent content = new StringContent(segmento,Encoding.UTF8,"application/json");
            var response = await _httpClient.PutAsync("api/Segmentos",content);
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar segmento");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }

    public async Task<SegmentoDTO> GetSegmento(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Segmentos/{id}");
            if(response.IsSuccessStatusCode)
            {
                if(response.StatusCode==System.Net.HttpStatusCode.NoContent)
                {
                    return default(SegmentoDTO);
                }
                return await response.Content.ReadFromJsonAsync<SegmentoDTO>();
            }
            else{
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro ao obter segmento de id {id}");
                throw new Exception($"status code: {response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            _logger.LogError("Erro ao acessar o produto");
            throw;
        }
    }

    public async Task<IEnumerable<SegmentoDTO>> GetSegmentos()
    {
        try
        {
            var segmentos = await _httpClient.GetFromJsonAsync<IEnumerable<SegmentoDTO>>("api/Segmentos");
            return segmentos;
        }
        catch (System.Exception)
        {
            _logger.LogError("Erro ao acessar segmentos da api");
            throw;
        }
    }

    public async Task DeletarSegmento(string id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Segmentos/{id}");
            if(!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar segmento");
                throw new Exception($"status code:{response.StatusCode} - {message}");
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
