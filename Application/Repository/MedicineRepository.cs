using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MedicineRepository : GenericRepository<Medicine>, IMedicine
{
    private readonly ApiDbContext _context;

    public MedicineRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<IEnumerable<Medicine>> GetMedicinesByLaboratory(int id)
    {
        var medicines = await _context.Medicines.Where(p=> p.LaboratoryId == id).ToListAsync();
        if(medicines.Any())
        {
            return medicines;
        }
        return null;
    }

    public async Task<IEnumerable<Medicine>> GetMedicinesPrice(double price)
    {
        var medicines  = await _context.Medicines
                        .Where(p=> p.Price >= price)
                        .Include(p=>p.Laboratory)
                        .ToListAsync();
        return medicines;
    }
}
