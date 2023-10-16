using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class LaboratoryDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Phone { get; set; }
}

public class LaboratoryPutDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Phone { get; set; }
}
