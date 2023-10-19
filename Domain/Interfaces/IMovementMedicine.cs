using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovementMedicine : IGenericRepository<MovementMedicine> 
{
    Task<string> RegisterAsync(MovementMedicine model);
    Task<IEnumerable<object>> GetMovementMedicines();
}
