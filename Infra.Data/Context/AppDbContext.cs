using Domain.Models;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Segmento> Segmentos { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);
        mb.Entity<Segmento>(new SegmentoMap().Configure);
        mb.Entity<Cliente>(new ClienteMap().Configure);
    }
}
