using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Reason : BaseEntity
{
    public string Name  { get; set; }
    public ICollection<Appoiment> Appoiments { get; set; }
}
