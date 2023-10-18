using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MovementTypeRepository : GenericRepository<MovementType>, IMovementType
{
    private readonly ApiDbContext _context;

    public MovementTypeRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<(int totalRegistros, IEnumerable<MovementType> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.MovementTypes as IQueryable<MovementType>;
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
    public override async Task<MovementType> GetByIdAsync(int id)
    {
        return await _context.MovementTypes
            .FirstOrDefaultAsync(p => p.Id == id);

    }
}
