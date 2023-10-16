using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Species: BaseEntity
{
    public string Name { get; set; }
    public ICollection<Breed> Breeds { get; set; }
}
