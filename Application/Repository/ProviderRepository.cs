using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class ProviderRepository : GenericRepository<Provider>, IProvider
{
    private readonly ApiDbContext _context;

    public ProviderRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public override async Task<Provider> GetByIdAsync(int id)
    {
        return await _context.Providers
            .Include(p => p.Medicines)
            .FirstOrDefaultAsync(p => p.Id == id);

    }

    public async Task<IEnumerable<Provider>> GetProvidersByMedicine(int id)
    {
        var providers = await _context.Providers
                        .Include(p=> p.Medicines)
                        .Where(p=> p.Medicines.Any(a=> a.Id == id))
                        .ToListAsync();
       return providers;
    }
}
