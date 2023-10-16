using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class ReasonRepository : GenericRepository<Reason>, IReason
{
    private readonly ApiDbContext _context;

    public ReasonRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
