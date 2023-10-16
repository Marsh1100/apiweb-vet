using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class MovementMedicineRepository : GenericRepository<MovementMedicine>, IMovementMedicine
{
    private readonly ApiDbContext _context;

    public MovementMedicineRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
