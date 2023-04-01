using Domain.Interfaces.Service;
using DTO;
using Services.Validator;
using FluentValidation;
using Domain.Interfaces.Repository;
using AutoMapper;
using Domain.Models;

namespace Services.Services;

public class SegmentoService : ISegmentoService
{
    private readonly ISegmentoRepository _repository;
    private readonly IMapper _mapper;

    public SegmentoService(ISegmentoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AddSegment(SegmentoDTO obj)
    {
        try
        {
            obj.Id = new Guid().ToString();
            new SegmentoValidator().ValidateAndThrow(obj);
            Segmento segmento = _mapper.Map<Segmento>(obj);
            await _repository.Add(segmento);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteSegment(SegmentoDTO obj)
    {
        try
        {
            Segmento segmento = await _repository.GetbyId(obj.Id);
            await _repository.Delete(segmento);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<SegmentoDTO> GetSegmento(string id)
    {
        try
        {
            Segmento obj = await _repository.GetbyId(id);
            SegmentoDTO robj = _mapper.Map<SegmentoDTO>(obj);
            return robj;
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public async Task<IEnumerable<SegmentoDTO>> GetSegmentos()
    {
        var objs = await _repository.GetAll();
        if(objs is null) 
            return null;
        List<SegmentoDTO> robjs = new List<SegmentoDTO>();
        foreach(var obj in objs)
        {
            robjs.Add(_mapper.Map<SegmentoDTO>(obj));
        }
        return robjs;
    }

    public async Task UpdateSegment(SegmentoDTO obj)
    {
        try
        {
            new SegmentoValidator().ValidateAndThrow(obj);
            Segmento segmento = await _repository.GetbyId(obj.Id);
            segmento.Nome=obj.Nome;
            segmento.Descricao=obj.Descricao;
            await _repository.Update(segmento);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
