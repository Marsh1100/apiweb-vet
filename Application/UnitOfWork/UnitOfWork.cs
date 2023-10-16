using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiDbContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    private IAppoiment _appoiments;
    private IBreed _breeds;
    private ILaboratory _laboratories;
    private IMedicine _medicines;
    private IMovementMedicine _movementMedicines;
    private IMovementType _movementType;
    private IOwner _owners;
    private IPet _pets;
    private IProvider _providers;
    private IReason _reasons;
    private ISpeciality _specialities;
    private ISpecies _speciesP;
    private IVet _veterinarians;
    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
    }
    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    public IAppoiment Appoiments
    {
        get
        {
            if (_appoiments == null)
            {
                _appoiments = new AppoimentRepository(_context);
            }
            return _appoiments;
        }
    }

    public IBreed Breeds
    {
        get
        {
            if (_breeds == null)
            {
                _breeds = new BreedRepository(_context);
            }
            return _breeds;
        }
    }
    public ILaboratory Laboratories
    {
        get
        {
            if (_laboratories == null)
            {
                _laboratories = new LaboratoryRepository(_context);
            }
            return _laboratories;
        }
    }
    public IMedicine Medicines
    {
        get
        {
            if (_medicines == null)
            {
                _medicines = new MedicineRepository(_context);
            }
            return _medicines;
        }
    }

    public IMovementMedicine MovementMedicines
    {
        get
        {
            if (_movementMedicines == null)
            {
                _movementMedicines = new MovementMedicineRepository(_context);
            }
            return _movementMedicines;
        }
    }
    public IMovementType MovementTypes
    {
        get
        {
            if (_movementType == null)
            {
                _movementType = new MovementTypeRepository(_context);
            }
            return _movementType;
        }
    }

    public IOwner Owners
    {
        get
        {
            if (_owners == null)
            {
                _owners = new OwnerRepository(_context);
            }
            return _owners;
        }
    }
    public IPet Pets
    {
        get
        {
            if (_pets == null)
            {
                _pets = new PetRepository(_context);
            }
            return _pets;
        }
    }

    public IProvider Providers
    {
        get
        {
            if (_providers == null)
            {
                _providers = new ProviderRepository(_context);
            }
            return _providers;
        }
    }
    public IReason Reasons
    {
        get
        {
            if (_reasons == null)
            {
                _reasons = new ReasonRepository(_context);
            }
            return _reasons;
        }
    }
    public ISpeciality Specialities
    {
        get
        {
            if (_specialities == null)
            {
                _specialities = new SpecialityRepository(_context);
            }
            return _specialities;
        }
    }
    public ISpecies SpeciesP
    {
        get
        {
            if (_speciesP == null)
            {
                _speciesP = new SpeciesRepository(_context);
            }
            return _speciesP;
        }
    }
     public IVet Veterinarians
    {
        get
        {
            if (_veterinarians == null)
            {
                _veterinarians = new VetRepository(_context);
            }
            return _veterinarians;
        }
    }

    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}