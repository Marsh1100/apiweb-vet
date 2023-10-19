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
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetBySpecieP(int id, int pageIndex, int pageSize, string search);
    Task<IEnumerable<Pet>> GetPetBySpecie();
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetPetBySpecieP(int pageIndex, int pageSize, string search);
    Task<IEnumerable<object>> GetPetsBySpecie();
    Task<IEnumerable<Pet>> GetOwnerPetsByBreed();
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetOwnerPetsByBreedP(int pageIndex, int pageSize, string search);
    Task<IEnumerable<Pet>> GetOwnerPetsByBreed(int id);
    Task<(int totalRegistros, IEnumerable<Pet> registros)> GetOwnerPetsByBreedP(int id, int pageIndex, int pageSize, string search);
    Task<IEnumerable<object>> GetQuantityPets();
    Task<IEnumerable<Appoiment>> GetPetsByAppoiment();
    Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByAppoimentP(int pageIndex, int pageSize, string search);
    Task<IEnumerable<Appoiment>> GetPetsByAppoiment(int quarter);
    Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByAppoimentP(int quarter, int pageIndex, int pageSize, string search);
    Task<IEnumerable<Appoiment>> GetPetsByVeterinarian(int id);
    Task<(int totalRegistros, IEnumerable<Appoiment> registros)> GetPetsByVeterinarianP(int id, int pageIndex, int pageSize, string search);

}
