using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class VetController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VetController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //CRUD
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<VetDto>>> Get()
    {
        var vets = await _unitOfWork.Veterinarians.GetAllAsync();
        return _mapper.Map<List<VetDto>>(vets);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VetAllDto>>> GetPagination([FromQuery] Params p)
    {
        var vets = await _unitOfWork.Veterinarians.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var vetsDto = _mapper.Map<List<VetAllDto>>(vets.registros);
        return  new Pager<VetAllDto>(vetsDto,vets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Vet>> Post([FromBody] VetDto vetDto)
    {
        var vet = _mapper.Map<Vet>(vetDto);
        this._unitOfWork.Veterinarians.Add(vet);
        await _unitOfWork.SaveAsync();

        if(vet == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=vet.Id}, vet);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Vet>> put(VetDto vetDto)
    {
        if(vetDto == null){ return NotFound(); }
        var vet = this._mapper.Map<Vet>(vetDto);
        this._unitOfWork.Veterinarians.Update(vet);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return vet;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var vet = await _unitOfWork.Veterinarians.GetByIdAsync(id);
        if(vet == null)
        {
            return NotFound();
        }
        this._unitOfWork.Veterinarians.Remove(vet);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //Endpoint
    //1. Visualización de los veterinarios cuya especialidad sea Cirugía vascular.
    [HttpGet("veterinariansBySpeciality/{id}")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetVeterinariansBySpecialty(int id)
    {
        var veterinarians =await _unitOfWork.Veterinarians.GetVeterinariansBySpecialty(id);
        var veterinariansDto = _mapper.Map<IEnumerable<VetSpecialityDto>>(veterinarians);

        return Ok(veterinariansDto);
    }
}
