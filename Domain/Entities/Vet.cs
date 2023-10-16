using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Vet : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int SpecialityId { get; set; }
    public Speciality Speciality {get; set; }

    public ICollection<Appoiment> Appoiments { get; set; }
}
