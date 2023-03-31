using Domain.Interfaces.Repository;
using Domain.Models;
using Infra.Data.Context;

namespace Infra.Data.Repository;

public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
{
    private readonly AppDbContext _context;
    public ClienteRepository(AppDbContext context) : base(context)
    {
        _context=context;
    }
}
