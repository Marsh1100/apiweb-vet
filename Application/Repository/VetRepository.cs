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
}
