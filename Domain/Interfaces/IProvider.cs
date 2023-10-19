using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IProvider : IGenericRepository<Provider> 
{
  Task<IEnumerable<Provider>> GetProvidersByMedicine(int id); 
  Task<(int totalRegistros, IEnumerable<Provider> registros)> GetProvidersByMedicineP(int id, int pageIndex, int pageSize, string search);

}
