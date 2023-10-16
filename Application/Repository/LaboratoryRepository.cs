using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class LaboratoryRepository : GenericRepository<Laboratory>, ILaboratory
{
    private readonly ApiDbContext _context;

    public LaboratoryRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
