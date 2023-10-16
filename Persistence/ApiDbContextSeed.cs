using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }catch(Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiDbContext>();
            logger.LogError(ex.Message);
        }
    } 
}
