using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class BreedRepository : GenericRepository<Breed>, IBreed
{
    private readonly ApiDbContext _context;

    public BreedRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
