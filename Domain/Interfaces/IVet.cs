using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IVet : IGenericRepository<Vet> 
{
    Task<IEnumerable<Vet>> GetVeterinariansBySpecialty(int id);
    Task<(int totalRegistros, IEnumerable<Vet> registros)> GetVeterinariansBySpecialtyP(int id, int pageIndex, int pageSize, string search);
    Task<IEnumerable<Vet>> GetVeterinariansBySpecialty();
    Task<(int totalRegistros, IEnumerable<Vet> registros)> GetVeterinariansBySpecialtyP(int pageIndex, int pageSize, string search);

}

