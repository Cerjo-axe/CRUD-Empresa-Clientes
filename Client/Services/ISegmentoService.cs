using DTO;

namespace Client.Services;

public interface ISegmentoService
{
    Task<IEnumerable<SegmentoDTO>> GetSegmentos();
    Task<SegmentoDTO> GetSegmento(string id);
    Task AddSegmento(SegmentoDTO obj);
    Task UpdateSegmento(SegmentoDTO obj);
    Task DeletarSegmento(string id);
}
