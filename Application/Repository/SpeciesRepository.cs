using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class SpeciesRepository : GenericRepository<Species>, ISpecies
{
    private readonly ApiDbContext _context;

    public SpeciesRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
