using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IAppoiment : IGenericRepository<Appoiment> 
{
    Task<string> RegisterAsync(Appoiment modelAppoiment);
}
