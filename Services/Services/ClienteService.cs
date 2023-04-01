using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Domain.Models;
using DTO;
using Services.Validator;
using FluentValidation;

namespace Services.Services;

public class ClienteService : IClienteService
{

    private readonly IClienteRepository _repository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AddCliente(ClienteDTO obj)
    {
        try
        {
            new ClienteValidator().ValidateAndThrow(obj);
            Cliente cliente = _mapper.Map<Cliente>(obj);
            await _repository.Add(cliente);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteCliente(ClienteDTO obj)
    {
        try
        {
            Cliente cliente = await _repository.GetbyId(obj.Id);
            await _repository.Delete(cliente);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ClienteDTO> GetCliente(string id)
    {
        try
        {
            Cliente obj = await _repository.GetbyId(id);
            ClienteDTO robj = _mapper.Map<ClienteDTO>(obj);
            return robj;
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public async Task<IEnumerable<ClienteDTO>> GetClientes()
    {
        try
        {
            var objs = await _repository.GetAll();
            if(objs is null) 
                return null;
            List<ClienteDTO> robjs = new List<ClienteDTO>();
            foreach(var obj in objs)
            {
                robjs.Add(_mapper.Map<ClienteDTO>(obj));
            }
            return robjs;
        }
        catch (System.Exception ex)
        {
            
            throw ex;
        }
    }

    public async Task UpdateCliente(ClienteDTO obj)
    {
        try
        {
            new ClienteValidator().ValidateAndThrow(obj);
            Cliente cliente = await _repository.GetbyId(obj.Id);
            cliente.Nome=obj.Nome;
            cliente.Site=obj.Site;
            cliente.SegmentoId=obj.SegmentoId;
            await _repository.Update(cliente);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
