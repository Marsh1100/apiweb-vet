using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPet : IGenericRepository<Pet> 
{
    Task<string> RegisterAsync(Pet modelPet);
    Task<IEnumerable<Pet>> GetPetBySpecie(int id);
    Task<IEnumerable<Pet>> GetPetBySpecie();
    Task<IEnumerable<object>> GetPetsBySpecie();
    Task<IEnumerable<Pet>> GetOwnerPetsByBreed();
    Task<IEnumerable<Pet>> GetOwnerPetsByBreed(int id);
    Task<IEnumerable<object>> GetQuantityPets();
    Task<IEnumerable<Appoiment>> GetPetsByAppoiment();
    Task<IEnumerable<Appoiment>> GetPetsByAppoiment(int quarter);
    Task<IEnumerable<Appoiment>> GetPetsByVeterinarian(int id);

}
