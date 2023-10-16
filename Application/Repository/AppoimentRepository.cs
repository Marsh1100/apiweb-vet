using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class AppoimentRepository : GenericRepository<Appoiment>, IAppoiment
{
    private readonly ApiDbContext _context;

    public AppoimentRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
