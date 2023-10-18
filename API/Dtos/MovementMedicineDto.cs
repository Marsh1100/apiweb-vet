using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class MovementMedicineDto
{
    public int Id { get; set; }
    [Required]
    public int MovementTypeId { get; set; }
    [Required]
    public int MedicineId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }

}
