using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MovementTypeRepository : GenericRepository<MovementType>, IMovementType
{
    private readonly ApiDbContext _context;

    public MovementTypeRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public override async Task<MovementType> GetByIdAsync(int id)
    {
        return await _context.MovementTypes
            .FirstOrDefaultAsync(p => p.Id == id);

    }
}
