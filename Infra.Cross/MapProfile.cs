using AutoMapper;
using Domain.Models;
using DTO;

namespace Infra.Cross;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Segmento,SegmentoDTO>().ReverseMap();
        CreateMap<Cliente,ClienteDTO>().ReverseMap();
    }
}
