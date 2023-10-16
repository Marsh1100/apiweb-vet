using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class MovementMedicineRepository : GenericRepository<MovementMedicine>, IMovementMedicine
{
    private readonly ApiDbContext _context;

    public MovementMedicineRepository(ApiDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<string> RegisterAsync(MovementMedicine model)
    {
        string strDate= model.Date.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
        
        var type = await _context.MovementTypes.Where(p=> p.Id == model.Id).FirstAsync();
        if (DateTime.TryParseExact(strDate, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, DateTimeStyles.None, out DateTime parseDate))
        {
            var medicineUpdate = await _context.Medicines.Where(p=> p.Id == model.MedicineId).FirstAsync();

            if(type.Name.ToUpper() == "Compra")
            {
                var newMovement = new MovementMedicine
                {
                    MedicineId = model.MedicineId,
                    Date = parseDate,
                    MovementTypeId = model.MovementTypeId,
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice,
                    Price = model.Quantity*model.UnitPrice
                };

                medicineUpdate.Stock += model.Quantity;
                _context.Medicines.Update(medicineUpdate);
                await _context.SaveChangesAsync();

                _context.MovementMedicines.Add(newMovement);
                await _context.SaveChangesAsync();
                return "Compra de medicamentos realizada con éxito!";

            }else if(type.Name.ToUpper() == "Venta")
            {
                if(medicineUpdate.Stock>= model.Quantity)
                {
                    var newMovement = new MovementMedicine
                    {
                        MedicineId = model.MedicineId,
                        Date = parseDate,
                        MovementTypeId = model.MovementTypeId,
                        Quantity = model.Quantity,
                        UnitPrice = medicineUpdate.Price,
                        Price = model.Quantity*medicineUpdate.Price
                    };
                    medicineUpdate.Stock -= model.Quantity;
                    _context.Medicines.Update(medicineUpdate);
                    await _context.SaveChangesAsync();

                    _context.MovementMedicines.Add(newMovement);
                    await _context.SaveChangesAsync();

                    return "Venta de medicamentos realizada con éxito!";

                }else
                {
                    return "No hay suficiente medicamentos para la venta";

                }
            }else
            {
                return "Lo siento. Este movimiento no esta disponible";
            }

        }else{
            return "La fecha no esta escrita en un formato correcto";
        }
    }
}
