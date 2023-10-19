using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicine : IGenericRepository<Medicine> 
{
    Task<IEnumerable<Medicine>> GetMedicinesByLaboratory();
    Task<IEnumerable<Medicine>> GetMedicinesByLaboratory(int id);
    Task<IEnumerable<Medicine>> GetMedicinesPrice();
    Task<IEnumerable<Medicine>> GetMedicinesPrice(double price);
}
