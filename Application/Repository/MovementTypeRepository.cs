using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class MovementTypeRepository : GenericRepository<MovementType>, IMovementType
{
    private readonly ApiDbContext _context;

    public MovementTypeRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
