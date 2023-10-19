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
    Task<IEnumerable<Vet>> GetVeterinariansBySpecialty();

}

