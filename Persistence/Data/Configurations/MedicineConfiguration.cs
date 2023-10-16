using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("medicine");
        builder.Property(p=> p.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(p=> p.Name)
            .IsUnique();
        builder.Property(p=> p.Price)
            .IsRequired();
        builder.Property(p=> p.Stock)
            .IsRequired();
        builder.HasOne(k=> k.Laboratory)
            .WithMany(c=> c.Medicines)
            .HasForeignKey(f=> f.LaboratoryId)
            .IsRequired();


         builder
            .HasMany(p=>p.Providers)
            .WithMany(p=>p.Medicines)
            .UsingEntity<MedicineProvider>(
                j=> j
                    .HasOne(t=> t.Provider)
                    .WithMany(m=> m.MedicineProviders)
                    .HasForeignKey(f=> f.ProviderId),
                j=> j
                    .HasOne(t => t.Medicine)
                    .WithMany(m=>m.MedicineProviders)
                    .HasForeignKey(f=> f.MedicineId),
                j=>
                {
                    j.ToTable("medicineProvider");
                    j.HasKey(t=> new {t.ProviderId, t.MedicineId});
                }
            );
    }
}
