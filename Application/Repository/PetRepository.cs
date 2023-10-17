using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IEnumerable<Pet>> GetPetBySpecie(int id)
    {
        var pets =await _context.Pets.ToListAsync();
        var breeds = await _context.Breeds.ToListAsync();
        var speciesP = await _context.SpeciesP.ToListAsync();
        
        var petsBreed = from pet in pets
                        join breed in breeds on pet.BreedId equals breed.Id
                        join species in speciesP on breed.SpeciesId equals species.Id
                        where species.Id == id
                        select pet;
        return petsBreed;
    }

    public async Task<IEnumerable<object>> GetPetsBySpecie()
    {
         var pets =await _context.Pets.ToListAsync();
        var breeds = await _context.Breeds.ToListAsync();
        var speciesP = await _context.SpeciesP.ToListAsync();
        
        /*var petsBreed = (from pet in pets
                        join breed in breeds on pet.BreedId equals breed.Id
                        join species in speciesP on breed.SpeciesId equals species.Id
                        select pet)
                        .GroupBy(g=> g.Breed.Species.Name);*/
        
        var petsBreed2 =(from pet in pets
                        join breed in breeds on pet.BreedId equals breed.Id
                        join species in speciesP on breed.SpeciesId equals species.Id
                        group pet by pet.Breed.Species.Name)
                        .Select(g=> new{
                            Species  = g.Key,
                            Pets = g.Select(s=> new
                                         {
                                            s.Name,
                                            Breed = s.Breed.Name,
                                            s.Birthdate

                                         })
                        });
    
                       
        return petsBreed2;
    }
}
