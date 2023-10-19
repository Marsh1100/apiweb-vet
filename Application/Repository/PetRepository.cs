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
    public override async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets.Include(p=>p.Owner).Include(p=>p.Breed).ThenInclude(p=>p.Species) as IQueryable<Pet>;
        return await Paginacion(query,pageIndex, pageSize, search);

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
            return "Máscota registrada con éxito!";


        }else{
            return "La fecha no esta escrita en un formato correcto";
        }
    }
    public async Task<IEnumerable<Pet>> GetPetBySpecie(int id)
    {
        var pets =await _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=>p.Species)
                            .Include(p=>p.Owner)
                            .Where(a=> a.Breed.Species.Id == id)
                            .ToListAsync();
        
        return pets;
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetBySpecieP(int id, int pageIndex, int pageSize, string search)
    {   
        var query = _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=>p.Species)
                            .Include(p=>p.Owner)
                            .Where(a=> a.Breed.Species.Id == id);
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    
    public async Task<IEnumerable<Pet>> GetPetBySpecie()
    {
        var pets =await _context.Pets.Include(p=>p.Owner).Include(p=>p.Breed).ThenInclude(p=>p.Species)
                        .Where(a=> a.Breed.Species.Name == "felina").ToListAsync();
     
        return pets;
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetBySpecieP(int pageIndex, int pageSize, string search)
    {   
        var query = _context.Pets.Include(p=>p.Owner).Include(p=>p.Breed).ThenInclude(p=>p.Species)
                        .Where(a=> a.Breed.Species.Name == "felina");
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    public async Task<IEnumerable<object>> GetPetsBySpecie()
    {
        var pets =await _context.Pets.ToListAsync();
        var breeds = await _context.Breeds.ToListAsync();
        var speciesP = await _context.SpeciesP.ToListAsync();
        
        var petsSpecie =(from pet in pets
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
    
                       
        return petsSpecie;
    }
    public async Task<IEnumerable<Pet>> GetOwnerPetsByBreed(int id)
    {
        var pets =await _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=> p.Species)
                            .Where(p=> p.Breed.Id == id)
                            .ToListAsync();
        return pets;
    }

    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetOwnerPetsByBreedP(int id, int pageIndex, int pageSize, string search)
    {  
        var query = _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=> p.Species)
                            .Where(p=> p.Breed.Id == id);
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    public async Task<IEnumerable<Pet>> GetOwnerPetsByBreed()
    {
        var pets =await _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=> p.Species)
                            .Where(p=> p.Breed.Name == "Golden Retriver")
                            .ToListAsync();
        return pets;
    }
    public async Task<(int totalRegistros, IEnumerable<Pet> registros)> GetOwnerPetsByBreedP(int pageIndex, int pageSize, string search)
    { 
        var query = _context.Pets
                            .Include(p=>p.Breed).ThenInclude(p=> p.Species)
                            .Where(p=> p.Breed.Name == "Golden Retriver");
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    public async Task<IEnumerable<object>> GetQuantityPets()
    {
        var pets =await _context.Pets.ToListAsync();
        var breeds = await _context.Breeds.ToListAsync();
        var speciesP = await _context.SpeciesP.ToListAsync();
        
        var petsBreed =(from pet in pets
                        join breed in breeds on pet.BreedId equals breed.Id
                        join species in speciesP on breed.SpeciesId equals species.Id
                        group pet by pet.Breed.Name)
                        .Select(g=> new{
                            Breed  = g.Key,
                            Quantity =g.Count(),
                            Pets = g.Select(s=> new
                                         {
                                            s.Name,
                                            s.Birthdate
                                         })
                        });
        return petsBreed;
    }

    public async Task<IEnumerable<Appoiment>> GetPetsByAppoiment(int quarter)
    {
        if (quarter <= 0 || quarter >= 5)
        {
            return null;
        }
        int init = 1;
        switch (quarter)
        {
            case 1: init = 1; break;
            case 2: init = 4; break;
            case 3: init = 7; break;
            case 4: init = 10; break;
            default:
                break;
        }
        DateTime dateStart = new(2023, init, 1);
        DateTime dateEnd = dateStart.AddMonths(3);

        var petsAppoiment = await _context.Appoiments
                    .Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p =>p.Breed)
                    .Where(a=> a.Date>= dateStart && a.Date<= dateEnd && a.Reason.Name == "Vacunación")
                    .ToListAsync();
        
        return petsAppoiment;
    }
    public async Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByAppoimentP(int quarter, int pageIndex, int pageSize, string search)
    {
        int init = 1;
        switch (quarter)
        {
            case 1: init = 1; break;
            case 2: init = 4; break;
            case 3: init = 7; break;
            case 4: init = 10; break;
            default:
                break;
        }
        DateTime dateStart = new(2023, init, 1);
        DateTime dateEnd = dateStart.AddMonths(3);

        var query = _context.Appoiments
                    .Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p =>p.Breed)
                    .Where(a=> a.Date>= dateStart && a.Date<= dateEnd && a.Reason.Name == "Vacunación");
        return await Paginacion(query,pageIndex, pageSize, search);
    }
    public async Task<IEnumerable<Appoiment>> GetPetsByAppoiment()
    {
        DateTime dateStart = new(2023, 1, 1);
        DateTime dateEnd = dateStart.AddMonths(3);

        var petsAppoiment = await _context.Appoiments
                    .Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p =>p.Breed)
                    .Where(a=> a.Date>= dateStart && a.Date<= dateEnd  && a.Reason.Name == "Vacunación")
                    .ToListAsync();
    
        return petsAppoiment;
    }
    public async Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByAppoimentP(int pageIndex, int pageSize, string search)
    {
        DateTime dateStart = new(2023, 1, 1);
        DateTime dateEnd = dateStart.AddMonths(3);

        var query =  _context.Appoiments
                    .Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p =>p.Breed)
                    .Where(a=> a.Date>= dateStart && a.Date<= dateEnd);
        
        return await Paginacion(query,pageIndex, pageSize, search);
    }

    public async Task<IEnumerable<Appoiment>> GetPetsByVeterinarian(int id)
    {
        var petsAppoiment = await _context.Appoiments
                    .Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p=>p.Breed)
                    .Where(p=> p.VetId == id)
                    .ToListAsync();
        return petsAppoiment;
    }
    public async Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByVeterinarianP(int id, int pageIndex, int pageSize, string search)
    {
        var query =  _context.Appoiments.Include(p=>p.Reason).Include(p=>p.Vet)
                    .Include(p=> p.Pet).ThenInclude(p=>p.Breed)
                    .Where(p=> p.VetId == id);
        
        return await Paginacion(query,pageIndex, pageSize, search);
    }

    private static async Task<(int totalRegistros, IEnumerable<Pet> registros)> Paginacion(IQueryable<Pet> query,int pageIndex, int pageSize, string search)
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
    private static async Task<(int totalRegistros, IEnumerable<Appoiment> registros)> Paginacion(IQueryable<Appoiment> query,int pageIndex, int pageSize, string search)
    {
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

   
}
