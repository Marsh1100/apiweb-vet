using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class AppoimentRepository : GenericRepository<Appoiment>, IAppoiment
{
    private readonly ApiDbContext _context;

    public AppoimentRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }
    public override async Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Appoiments.Include(p=>p.Pet).Include(p=>p.Reason).Include(p=>p.Vet) as IQueryable<Appoiment>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Pet.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<string> RegisterAsync(Appoiment modelAppoiment)
    {
        
        string strDate= modelAppoiment.Date.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
        
        if (DateTime.TryParseExact(strDate, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, DateTimeStyles.None, out DateTime parseDate))
        {
            var newAppoiment = new Appoiment
            {
                PetId = modelAppoiment.PetId,
                Date = parseDate,
                ReasonId = modelAppoiment.ReasonId,
                VetId = modelAppoiment.VetId

            };
            _context.Appoiments.Add(newAppoiment);
            await _context.SaveChangesAsync();
            return "Cita asignada con exito!";


        }else{
            return "La fecha no esta escrita en un formato correcto";
        }

    }
}
