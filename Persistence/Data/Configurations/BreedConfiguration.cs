using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("bread");
        builder.Property(p=> p.Id)
            .IsRequired();

        builder.Property(p=> p.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasOne(k=> k.Species)
            .WithMany(c=> c.Breeds)
            .HasForeignKey(f=> f.SpeciesId)
            .IsRequired();

    }
}
