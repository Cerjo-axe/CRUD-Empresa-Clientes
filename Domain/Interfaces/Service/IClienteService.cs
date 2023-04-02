using DTO;

namespace Domain.Interfaces.Service;

public interface IClienteService
{
    Task AddCliente(ClienteDTO obj);
    Task<IEnumerable<ClienteDTO>> GetClientes();
    Task<ClienteDTO> GetCliente(string id);
    Task UpdateCliente(ClienteDTO obj);
    Task DeleteCliente(string id);
}
