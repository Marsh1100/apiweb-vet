using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUserRepository Users { get; }
    IAppoiment Appoiments { get; }
    IBreed Breeds { get; }
    ILaboratory Laboratories { get; }
    IMedicine Medicines { get; }
    IMovementMedicine MovementMedicines { get; }
    IMovementType MovementTypes { get; }
    IOwner Owners { get; }
    IProvider Providers { get; }
    IReason Reasons { get; }
    ISpeciality Specialities { get; }
    ISpecies SpeciesP { get; }
    IVet Veterinarians { get; }
    Task<int> SaveAsync();
}
