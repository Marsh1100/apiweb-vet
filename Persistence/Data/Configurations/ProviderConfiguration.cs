using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.ToTable("provider");
        builder.Property(p=> p.Id)
            .IsRequired();
        builder.Property(p=> p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p=> p.Address)
            .IsRequired();
    }
}
