using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class MedicineRepository : GenericRepository<Medicine>, IMedicine
{
    private readonly ApiDbContext _context;

    public MedicineRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
