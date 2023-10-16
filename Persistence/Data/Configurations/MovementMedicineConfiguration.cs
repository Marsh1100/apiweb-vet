using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class MovementDetailConfiguration : IEntityTypeConfiguration<MovementMedicine>
{
    public void Configure(EntityTypeBuilder<MovementMedicine> builder)
    {
        builder.ToTable("movementMedicine");
        builder.Property(p=> p.Id)
            .IsRequired();
        builder.Property(p=> p.Quantity)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p=> p.UnitPrice)
            .IsRequired();
        builder.Property(p=> p.Price)
            .IsRequired();
        builder.HasOne(k=> k.MovementType)
            .WithMany(c=> c.MovementMedicines)
            .HasForeignKey(f=> f.MovementTypeId)
            .IsRequired();

    }
}
