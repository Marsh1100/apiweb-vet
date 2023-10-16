using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class VetRepository : GenericRepository<Vet>, IVet
{
    private readonly ApiDbContext _context;

    public VetRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
