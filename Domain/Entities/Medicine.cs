using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Medicine : BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public int LaboratoryId { get; set; }
    public Laboratory Laboratory { get; set; }
    public ICollection<MovementMedicine> MovementMedicines { get; set; }
    public ICollection<MedicineProvider> MedicineProviders { get; set; }
    public ICollection<Provider> Providers { get; set; }  = new HashSet<Provider>();
}
