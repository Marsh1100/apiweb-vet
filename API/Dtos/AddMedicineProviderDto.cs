
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class AddMedicineProviderDto
{
    [Required]
    public string Medicine { get; set; }
    [Required]
    public int ProviderId { get; set; }
}