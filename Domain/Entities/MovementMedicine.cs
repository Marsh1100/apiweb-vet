using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

public class MovementMedicine : BaseEntity
{
    public int MedicineId { get; set;}
    public Medicine Medicine {get; set;}
    public DateTime Date {get; set;}
    public int MovementTypeId {get; set;}
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double Price { get; set; }
    public MovementType MovementType {get; set;}

}
