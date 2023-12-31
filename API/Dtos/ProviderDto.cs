using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class ProviderDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
   
}
public class ProviderAllDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public IEnumerable<MedicineBaseDto> Medicines { get; set; }
   
}

public class ProviderPutDto
{
    [Required]
    public int Id { get; set; }
   [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
   
}