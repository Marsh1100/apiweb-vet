using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class OwnerRepository : GenericRepository<Owner>, IOwner
{
    private readonly ApiDbContext _context;

    public OwnerRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Owner>> GetOwnerPets()
    {
        var ownerPets = await _context.Owners
                        .Include(p=> p.Pets).ThenInclude(d=> d.Breed)
                        .ToListAsync();
        return ownerPets;
    }

}
