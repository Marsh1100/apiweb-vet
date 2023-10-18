using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class VetDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public int SpecialityId { get; set; }  

}
public class VetSpecialityDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Phone { get; set; } 

}
public class VetAllDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; } 

}
