using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class AppoimentDto
{
    [Required]
    public int PetId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int ReasonId { get; set; }
    [Required]
    public int VetId { get; set; }

}
