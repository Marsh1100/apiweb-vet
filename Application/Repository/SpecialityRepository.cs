using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class SpecialityRepository : GenericRepository<Speciality>, ISpeciality
{
    private readonly ApiDbContext _context;

    public SpecialityRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
}
