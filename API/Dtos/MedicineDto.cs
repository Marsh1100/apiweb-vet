using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class MedicineBaseDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Stock { get; set; }
    [Required]
    public double Price { get; set; }
}

public class MedicineDto : MedicineBaseDto
{
    [Required]
    public int LaboratoryId { get; set; }
}
public class MedicinePriceDto : MedicineBaseDto
{
    public string Laboratory { get; set; }
}

