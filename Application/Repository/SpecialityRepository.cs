using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SpecialityRepository : GenericRepository<Speciality>, ISpeciality
{
    private readonly ApiDbContext _context;

    public SpecialityRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Speciality> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Specialities as IQueryable<Speciality>;
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
