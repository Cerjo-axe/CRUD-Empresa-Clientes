using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(p=>p.Id);
        builder.Property(p=>p.Nome).IsRequired();
        builder.HasIndex(p=>p.Nome).IsUnique();
        builder.Property(p=>p.Site).IsRequired();
        builder.HasIndex(p=>p.Site).IsUnique();
        builder.HasOne(p=>p.Segmento).WithMany(p=>p.Clientes).HasForeignKey(p=>p.SegmentoId).IsRequired();
    }
}
