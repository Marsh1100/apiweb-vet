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
   // Task<(int totalRegistros, IEnumerable<object> registros)> GetMovementMedicinesP(int pageIndex, int pageSize, string search);

}
