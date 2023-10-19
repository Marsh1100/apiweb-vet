using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IOwner : IGenericRepository<Owner> 
{
    Task<IEnumerable<Owner>> GetOwnerPets();
    Task<(int totalRegistros, IEnumerable<Owner> registros)> GetOwnerPetsP(int pageIndex, int pageSize, string search);

}
