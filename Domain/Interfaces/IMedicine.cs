using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IMedicine : IGenericRepository<Medicine> 
{
    Task<IEnumerable<Medicine>> GetMedicinesByLaboratory();
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesByLaboratoryP(int pageIndex, int pageSize, string search);

    Task<IEnumerable<Medicine>> GetMedicinesByLaboratory(int id);
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesByLaboratoryP(int id, int pageIndex, int pageSize, string search);

    Task<IEnumerable<Medicine>> GetMedicinesPrice();
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesPriceP( int pageIndex, int pageSize, string search);

    Task<IEnumerable<Medicine>> GetMedicinesPrice(double price);
    Task<(int totalRegistros, IEnumerable<Medicine> registros)> GetMedicinesPriceP(double price, int pageIndex, int pageSize, string search);

}
