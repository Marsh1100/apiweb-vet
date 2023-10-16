using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace Domain.Entities;

public class Appoiment : BaseEntity
{
    public int PetId { get; set; }
    public Pet Pet { get; set; }
    public DateTime Date { get; set; }
    public int ReasonId { get; set; }
    public Reason Reason { get; set; }
    public int VetId { get; set; }
    public Vet Vet {get; set; }
}
