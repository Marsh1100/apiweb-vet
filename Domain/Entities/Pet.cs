using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities;

public class Pet : BaseEntity
{
    public int OwnerId { get; set; }
    public Owner Owner { get; set;}
    public int BreedId { get; set; }
    public Breed Breed { get; set;}
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }

    public ICollection<Appoiment> Appoiments { get; set; }
}
