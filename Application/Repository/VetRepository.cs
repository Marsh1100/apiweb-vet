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
        return await Paginacion(query,pageIndex, pageSize, search);

    }

    public async Task<IEnumerable<Vet>> GetVeterinariansBySpecialty(int id)
    {

        var vet = await _context.Veterinarians
                        .Include(p=>p.Speciality)
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
                        .Include(p=>p.Speciality)
                        .Where(p=> p.Speciality.Name == "Cirugía vascular")
                        .ToListAsync();
        if (vet.Any())
        {
            return vet;
        }
        return null;
    }
    public async Task<(int totalRegistros, IEnumerable<Vet> registros)> GetVeterinariansBySpecialtyP(int id, int pageIndex, int pageSize, string search)
    {
        var query =  _context.Veterinarians
                        .Include(p=>p.Speciality)
                        .Where(p=> p.Speciality.Id == id);
        return await Paginacion(query,pageIndex, pageSize, search);

    }
    public async Task<(int totalRegistros, IEnumerable<Vet> registros)> GetVeterinariansBySpecialtyP(int pageIndex, int pageSize, string search)
    {

        var query =  _context.Veterinarians
                        .Include(p=>p.Speciality)
                        .Where(p=> p.Speciality.Name == "Cirugía vascular");
       
        return await Paginacion(query,pageIndex, pageSize, search);
    }

    private static async Task<(int totalRegistros, IEnumerable<Vet> registros)> Paginacion(IQueryable<Vet> query,int pageIndex, int pageSize, string search)
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
