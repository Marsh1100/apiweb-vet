using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class PetDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int OwnerId { get; set; }  
    [Required]
    public int BreedId { get; set; }  
    [Required]
    public DateTime Birthdate { get; set; }

}
public class PetsOnlyDto
{
    public int Id { get; set; }
    public string Owner  {get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }  
    public DateTime Birthdate { get; set; }

}

public class PetsOwnerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }  
    public string Species { get; set; }
    public DateTime Birthdate { get; set; }
}

public class PetsAppoimentDto
{
    public int Id { get; set; }
    public DateTime Date  {get; set;}
    public string Name { get; set; }
    public string Breed { get; set; }  
    public DateTime Birthdate { get; set; }

}
