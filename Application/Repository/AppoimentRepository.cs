using System.Globalization;
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
