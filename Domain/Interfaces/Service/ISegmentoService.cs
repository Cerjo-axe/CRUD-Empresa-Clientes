using DTO;

namespace Domain.Interfaces.Service;

public interface ISegmentoService
{
    Task AddSegment(SegmentoDTO obj);
    Task<IEnumerable<SegmentoDTO>> GetSegmentos();
    Task<SegmentoDTO> GetSegmento(string id);
    Task UpdateSegment(SegmentoDTO obj);
    Task DeleteSegment(string id);
    
}
