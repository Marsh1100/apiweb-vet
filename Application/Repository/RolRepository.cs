using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly ApiDbContext _context;

    public RolRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
