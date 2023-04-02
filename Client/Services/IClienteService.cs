using DTO;

namespace Client.Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteDTO>> GetClientes();
    Task<ClienteDTO> GetCliente(string id);
    Task AddCliente(ClienteDTO obj);
    Task UpdateCliente(ClienteDTO obj);
    Task DeletarCliente(string id);
}
