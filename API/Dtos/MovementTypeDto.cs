using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class MovementTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
