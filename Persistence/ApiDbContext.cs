using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Persistence;

public class ApiDbContext : DbContext 
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UserRol> UserRoles { get; set; }

    public DbSet<Appoiment> Appoiments { get; set; }
    public DbSet<Vet> Veterinarians { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Species> SpeciesP { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<MovementMedicine> MovementMedicines { get; set; }
    public DbSet<MovementType> MovementTypes { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<MedicineProvider> MedicineProviders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
