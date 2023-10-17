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

}
