using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class PetController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PetController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PetDto>>> Get()
    {
        var appoiments = await _unitOfWork.Pets.GetAllAsync();
        return _mapper.Map<List<PetDto>>(appoiments);
    }
     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsOnlyDto>>> GetPagination([FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsOnlyDto>>(pets.registros);
        return  new Pager<PetsOnlyDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] PetDto petDto)
    {
        var pet = _mapper.Map<Pet>(petDto);
        var result =await _unitOfWork.Pets.RegisterAsync(pet);

        return Ok(result);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<PetDto>> put(PetDto petDto)
    {
        if(petDto == null){ return NotFound(); }
        var pet = this._mapper.Map<Pet>(petDto);
        this._unitOfWork.Pets.Update(pet);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        var pet2dto = _mapper.Map<PetDto>(pet);
        return pet2dto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var pet = await _unitOfWork.Pets.GetByIdAsync(id);
        if(pet == null)
        {
            return NotFound();
        }
        this._unitOfWork.Pets.Remove(pet);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }
    //3. Mascotas que se encuentren registradas cuya especie sea felina.
    [HttpGet("petBySpecie/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetPetBySpecie(int id)
    {
        var pets =await _unitOfWork.Pets.GetPetBySpecie(id);
        var petsDto = _mapper.Map<IEnumerable<PetsOnlyDto>>(pets);

        return Ok(petsDto);
    }

    //6.Lista de las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
    [HttpGet("petsByAppoiment/{quarter}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetPetsByAppoiment(int quarter)
    {
        var pets =await _unitOfWork.Pets.GetPetsByAppoiment(quarter);
        var petsDto = _mapper.Map<IEnumerable<PetsAppoimentDto>>(pets);

        return Ok(petsDto);
    }

     //7.Lista de todas las mascotas agrupadas por especie.
    [HttpGet("petsBySpecies")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetPetsBySpecie()
    {
        var pets =await _unitOfWork.Pets.GetPetsBySpecie();

        return Ok(pets);
    }
     //9.Lista de las mascotas que fueron atendidas por un determinado veterinario.
    [HttpGet("petsByVeterinarian/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetPetsByVeterinarian(int id)
    {
        var pets =await _unitOfWork.Pets.GetPetsByVeterinarian(id);
        var petsDto = _mapper.Map<IEnumerable<PetsAppoimentDto>>(pets);

        return Ok(petsDto);
    }

    //11.Lista de las mascotas y sus propietarios cuya raza sea Golden Retriver
    [HttpGet("ownerPetsByBreed/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetOwnerPetsByBreed(int id)
    {
        var pets =await _unitOfWork.Pets.GetOwnerPetsByBreed(id);
        var petsDto = _mapper.Map<IEnumerable<PetsOwnerDto>>(pets);

        return Ok(petsDto);
    }
    //12.Lista de la cantidad de mascotas que pertenecen a una raza.
    [HttpGet("quantityPets")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetQuantityPets()
    {
        var pets =await _unitOfWork.Pets.GetQuantityPets();
        return Ok(pets);
    }
}
