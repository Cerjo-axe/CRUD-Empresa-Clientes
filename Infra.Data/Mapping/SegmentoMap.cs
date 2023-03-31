using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Mapping;

public class SegmentoMap : IEntityTypeConfiguration<Segmento>
{
    public void Configure(EntityTypeBuilder<Segmento> builder)
    {
        builder.HasKey(p=>p.Id);
        builder.Property(p=>p.Nome).IsRequired();
        builder.HasIndex(p=>p.Nome).IsUnique();
        builder.Property(p=>p.Descricao).HasMaxLength(50);
    }
}
