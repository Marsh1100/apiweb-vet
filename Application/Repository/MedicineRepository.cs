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
    public override async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines as IQueryable<Medicine>;
        return await Paginacion(query,pageIndex, pageSize, search);

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
    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesByLaboratoryP(int id, int pageIndex, int pageSize, string search)
    {
        var query =  _context.Medicines.Where(p=> p.LaboratoryId == id);
        return await Paginacion(query,pageIndex, pageSize, search);

    }
    public async Task<IEnumerable<Medicine>> GetMedicinesByLaboratory()
    {
        var medicines = await _context.Medicines.Where(p=> p.Laboratory.Name == "Genfar").ToListAsync();
        if(medicines.Any())
        {
            return medicines;
        }
        return null;
    }

    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesByLaboratoryP(int pageIndex, int pageSize, string search)
    {
        var query = _context.Medicines.Where(p=> p.Laboratory.Name == "Genfar");
        return await Paginacion(query,pageIndex, pageSize, search);

    }

    public async Task<IEnumerable<Medicine>> GetMedicinesPrice(double price)
    {
        var medicines  = await _context.Medicines
                        .Where(p=> p.Price >= price)
                        .Include(p=>p.Laboratory)
                        .ToListAsync();
        return medicines;
    }
    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesPriceP(double price, int pageIndex, int pageSize, string search)
    {
        var query  =  _context.Medicines
                        .Where(p=> p.Price >= price)
                        .Include(p=>p.Laboratory);
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    
    public async Task<IEnumerable<Medicine>> GetMedicinesPrice()
    {
        var medicines  = await _context.Medicines
                        .Where(p=> p.Price >= 50000)
                        .Include(p=>p.Laboratory)
                        .ToListAsync();
        return medicines;
    }
    public async Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesPriceP(int pageIndex, int pageSize, string search)
    {
        var query  =  _context.Medicines
                        .Where(p=> p.Price >= 50000)
                        .Include(p=>p.Laboratory); 
        return await Paginacion(query,pageIndex, pageSize, search);
    }

    private static async Task<(int totalRegistros, IEnumerable<Medicine> registros)> Paginacion(IQueryable<Medicine> query,int pageIndex, int pageSize, string search)
    {
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }
    
}
