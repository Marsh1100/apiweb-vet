using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class VetRepository : GenericRepository<Vet>, IVet
{
    private readonly ApiDbContext _context;

    public VetRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<(int totalRegistros, IEnumerable<Vet> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Veterinarians.Include(p=>p.Speciality) as IQueryable<Vet>;
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

    public async Task<IEnumerable<Vet>> GetVeterinariansBySpecialty(int id)
    {

        var vet = await _context.Veterinarians
                        .Where(p=> p.SpecialityId == id)
                        .ToListAsync();
        if (vet.Any())
        {
            return vet;
        }
        return null;
    }
    public async Task<IEnumerable<Vet>> GetVeterinariansBySpecialty()
    {

        var vet = await _context.Veterinarians
                        .Where(p=> p.Speciality.Name == "Cirugía vascular")
                        .ToListAsync();
        if (vet.Any())
        {
            return vet;
        }
        return null;
    }
}
