using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class BreedRepository : GenericRepository<Breed>, IBreed
{
    private readonly ApiDbContext _context;

    public BreedRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<(int totalRegistros, IEnumerable<Breed> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Breeds.Include(p=>p.Species) as IQueryable<Breed>;
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
}
