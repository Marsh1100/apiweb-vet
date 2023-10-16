using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class MovementType : BaseEntity
{
    public string Name { get; set; }
    public ICollection<MovementMedicine> MovementMedicines { get; set; }
}
