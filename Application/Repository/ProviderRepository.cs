using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class ProviderRepository : GenericRepository<Provider>, IProvider
{
    private readonly ApiDbContext _context;

    public ProviderRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
