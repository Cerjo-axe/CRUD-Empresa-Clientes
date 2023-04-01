using Domain.Models;
using Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=CRUD;User Id=sergioluiz;Password=admin;");

            return new AppDbContext(optionsBuilder.Options);
        }
}
