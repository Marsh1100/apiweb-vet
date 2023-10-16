using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class PetRepository : GenericRepository<Pet>, IPet
{
    private readonly ApiDbContext _context;

    public PetRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
