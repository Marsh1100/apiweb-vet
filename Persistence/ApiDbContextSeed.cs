using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Persistence;

public class ApiDbContextSeed
{
    public static async Task SeedAsync(ApiDbContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Rol>
                {
                    new() { Name = "Administrator" },
                    new() { Name = "Employee" },
                    new() { Name = "WithoutRol" }
                };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
            if (!context.MovementTypes.Any())
            {
                var movementTypes = new List<MovementType>
                {
                    new() { Name = "Compra" },
                    new() { Name = "Venta" }
                };
                context.MovementTypes.AddRange(movementTypes);
                await context.SaveChangesAsync();
            }
            if (!context.Reasons.Any())
            {
                var reasons = new List<Reason>
                {
                    new() { Name = "Vacunación" },
                    new() { Name = "Control" },
                    new() { Name = "Abrir historia clínica" }
                };
                context.Reasons.AddRange(reasons);
                await context.SaveChangesAsync();
            }

        }catch(Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiDbContext>();
            logger.LogError(ex.Message);
        }
        try
        {
            if(!context.Users.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/user.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<User>();
                        List<User> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new User
                            {
                                Id = item.Id,
                                IdenNumber = item.IdenNumber,
                                UserName=item.UserName,
                                Email = item.Email,
                                Password = item.Password
                            });
                        }
                        context.Users.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.UserRoles.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/userrol.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<UserRol>();
                        List<UserRol> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new UserRol
                            {
                                UserId= item.UserId,
                                RolId = item.RolId
                            });
                        }
                        context.UserRoles.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Specialities.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/speciality.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Speciality>();
                        List<Speciality> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Speciality
                            {
                                Id = item.Id,
                                Name = item.Name
                            });
                        }
                        context.Specialities.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Veterinarians.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/vet.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Vet>();
                        List<Vet> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Vet
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Email = item.Email,
                                Phone = item.Phone,
                                SpecialityId = item.SpecialityId
                            });
                        }
                        context.Veterinarians.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Laboratories.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/laboratory.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Laboratory>();
                        List<Laboratory> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Laboratory
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Address = item.Address,
                                Phone = item.Phone
                            });
                        }
                        context.Laboratories.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Medicines.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/medicine.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Medicine>();
                        List<Medicine> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Medicine
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Stock = item.Stock,
                                Price = item.Price,
                                LaboratoryId = item.LaboratoryId
                            });
                        }
                        context.Medicines.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Providers.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/provider.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Provider>();
                        List<Provider> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Provider
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Address = item.Address
                            });
                        }
                        context.Providers.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.MedicineProviders.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/medicineProvider.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<MedicineProvider>();
                        List<MedicineProvider> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new MedicineProvider
                            {
                                MedicineId = item.MedicineId,
                                ProviderId = item.ProviderId
                            });
                        }
                        context.MedicineProviders.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.MovementMedicines.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/movementMedicine.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<MovementMedicine>();
                        List<MovementMedicine> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new MovementMedicine
                            {
                                MedicineId = item.MedicineId,
                                Date = item.Date,
                                MovementTypeId = item.MovementTypeId,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                Price = item.Price
                            });
                        }
                        context.MovementMedicines.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.SpeciesP.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/species.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Species>();
                        List<Species> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Species
                            {
                                Id = item.Id,
                                Name = item.Name
                            });
                        }
                        context.SpeciesP.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Owners.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/owner.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Owner>();
                        List<Owner> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Owner
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Email = item.Email,
                                Phone = item.Phone
                            });
                        }
                        context.Owners.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Breeds.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/breed.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Breed>();
                        List<Breed> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Breed
                            {
                                Id = item.Id,
                                Name = item.Name,
                                SpeciesId = item.SpeciesId
                            });
                        }
                        context.Breeds.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if(!context.Pets.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/pet.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Pet>();
                        List<Pet> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Pet
                            {
                                Id = item.Id,
                                Name = item.Name,
                                OwnerId = item.OwnerId,
                                BreedId = item.BreedId,
                                Birthdate = item.Birthdate
                            });
                        }
                        context.Pets.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }
            
            if(!context.Appoiments.Any())
            {
                using (var reader = new StreamReader("../Persistence/Data/Csvs/appoiment.csv"))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HeaderValidated = null, // Esto deshabilita la validación de encabezados
                        MissingFieldFound = null
                    }))
                    {
                        // Resto de tu código para leer y procesar el archivo CSV
                        var list = csv.GetRecords<Appoiment>();
                        List<Appoiment> entidad = new();
                        foreach (var item in list)
                        {
                            entidad.Add(new Appoiment
                            {
                                Id = item.Id,
                                PetId = item.PetId,
                                ReasonId = item.ReasonId,
                                VetId = item.VetId,
                                Date = item.Date
                            });
                        }
                        context.Appoiments.AddRange(entidad);
                        await context.SaveChangesAsync();
                    }
                }
            }

        }catch(Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiDbContext>();
            logger.LogError(ex.Message);
        }
        
    } 
}
