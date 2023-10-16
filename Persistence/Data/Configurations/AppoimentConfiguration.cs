using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class AppoimentConfiguration : IEntityTypeConfiguration<Appoiment>
{
    public void Configure(EntityTypeBuilder<Appoiment> builder)
    {
        builder.ToTable("appoiment");
        builder.Property(p=> p.Id)
            .IsRequired();
        
        builder.Property(p=> p.Date)
            .IsRequired();
        
        builder.HasOne(k=> k.Pet)
            .WithMany(c=> c.Appoiments)
            .HasForeignKey(f=> f.PetId)
            .IsRequired();

        builder.HasOne(k=> k.Reason)
            .WithMany(c=> c.Appoiments)
            .HasForeignKey(f=> f.ReasonId)
            .IsRequired();

        builder.HasOne(k=> k.Vet)
            .WithMany(c=> c.Appoiments)
            .HasForeignKey(f=> f.VetId)
            .IsRequired();

        
    }
}
