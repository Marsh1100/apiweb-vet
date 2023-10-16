using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class OwnerRepository : GenericRepository<Owner>, IOwner
{
    private readonly ApiDbContext _context;

    public OwnerRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
