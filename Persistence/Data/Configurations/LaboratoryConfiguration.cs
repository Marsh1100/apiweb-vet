using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class LaboratoryConfiguration : IEntityTypeConfiguration<Laboratory>
{
    public void Configure(EntityTypeBuilder<Laboratory> builder)
    {
        builder.ToTable("laboratory");
        builder.Property(p=> p.Id)
            .IsRequired();
        builder.Property(p=> p.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(p=> p.Name)
            .IsUnique();
        builder.Property(p=> p.Address)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p=> p.Phone)
            .HasMaxLength(15)
            .IsRequired();

    }
}
