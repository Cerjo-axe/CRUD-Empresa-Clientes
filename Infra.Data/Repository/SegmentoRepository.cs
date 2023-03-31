using Domain.Interfaces.Repository;
using Domain.Models;
using Infra.Data.Context;

namespace Infra.Data.Repository;

public class SegmentoRepository : BaseRepository<Segmento>, ISegmentoRepository
{
    private readonly AppDbContext _context;
    public SegmentoRepository(AppDbContext context) : base(context)
    {
        _context=context;
    }
}
