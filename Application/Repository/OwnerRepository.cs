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
    public override async Task<(int totalRegistros, IEnumerable<Owner> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Owners as IQueryable<Owner>;
        return await Paginacion(query,pageIndex, pageSize, search);

    }
    public async Task<IEnumerable<Owner>> GetOwnerPets()
    {
        var ownerPets = await _context.Owners
                        .Include(p=> p.Pets).ThenInclude(d=> d.Breed).ThenInclude(d=>d.Species)
                        .ToListAsync();
        return ownerPets;
    }

    public async Task<(int totalRegistros, IEnumerable<Owner> registros)> GetOwnerPetsP(int pageIndex, int pageSize, string search)
    {
        var query = _context.Owners
                        .Include(p=> p.Pets).ThenInclude(d=> d.Breed).ThenInclude(d=>d.Species);
        return await Paginacion(query,pageIndex, pageSize, search);

    }

    private static async Task<(int totalRegistros, IEnumerable<Owner> registros)> Paginacion(IQueryable<Owner> query,int pageIndex, int pageSize, string search)
    {
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
