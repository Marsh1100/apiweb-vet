using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{
    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.ToTable("vet");
        builder.Property(p=> p.Id)
            .IsRequired();
        builder.Property(p=> p.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(p=> p.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p=> p.Phone)
            .HasMaxLength(15)
            .IsRequired();
        builder.HasOne(k=> k.Speciality)
            .WithMany(c=> c.Veterinarians)
            .HasForeignKey(f=> f.SpecialityId)
            .IsRequired();
    }   
}
