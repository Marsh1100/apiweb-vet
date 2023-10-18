using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratory
{
    private readonly ApiDbContext _context;

    public LaboratoryRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<(int totalRegistros, IEnumerable<Laboratory> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Laboratories as IQueryable<Laboratory>;
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
