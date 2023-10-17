using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class MedicineDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int LaboratoryId { get; set; }
}

public class MedicineByLabDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
}
