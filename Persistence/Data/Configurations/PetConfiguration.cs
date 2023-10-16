using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pet");
        builder.Property(p=> p.Id)
            .IsRequired();
        builder.Property(p=> p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p=> p.Birthdate)
            .IsRequired();
        builder.HasOne(k=> k.Owner)
            .WithMany(c=> c.Pets)
            .HasForeignKey(f=> f.OwnerId)
            .IsRequired();

        builder.HasOne(k=> k.Breed)
            .WithMany(c=> c.Pets)
            .HasForeignKey(f=> f.BreedId)
            .IsRequired();

    }
}
