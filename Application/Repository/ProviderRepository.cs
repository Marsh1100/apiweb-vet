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
    public override async Task<(int totalRegistros, IEnumerable<Provider> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Providers as IQueryable<Provider>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
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
