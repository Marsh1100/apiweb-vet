using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class PetRepository : GenericRepository<Pet>, IPet
{
    private readonly ApiDbContext _context;

    public PetRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<string> RegisterAsync(Pet modelPet)
    {
        string strDate= modelPet.Birthdate.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
        
        if (DateTime.TryParseExact(strDate, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, DateTimeStyles.None, out DateTime parseDate))
        {
            var newPet = new Pet
            {
                OwnerId = modelPet.OwnerId,
                BreedId = modelPet.BreedId,
                Name = modelPet.Name,
                Birthdate = parseDate

            };
            _context.Pets.Add(newPet);
            await _context.SaveChangesAsync();
            return "Cita asignada con exito!";


        }else{
            return "La fecha no esta escrita en un formato correcto";
        }
    }
}
