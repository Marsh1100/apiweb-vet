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
    [HttpGet("petBySpecie")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPetBySpecie()
    {
        var pets =await _unitOfWork.Pets.GetPetBySpecie();
        var petsDto = _mapper.Map<IEnumerable<PetsOnlyDto>>(pets);

        return Ok(petsDto);
    }
    [HttpGet("petBySpecie")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsOnlyDto>>> GetPagination2([FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetPetBySpecieP(p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsOnlyDto>>(pets.registros);
        return  new Pager<PetsOnlyDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }
    //3.1 Mascotas que se encuentren registradas de una especie.
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
    [HttpGet("petBySpecie/{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsOnlyDto>>> GetPagination3(int id,[FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetPetBySpecieP(id,p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsOnlyDto>>(pets.registros);
        return  new Pager<PetsOnlyDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }


    //6. Lista de las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
    [HttpGet("petsByAppoiment")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPetsByAppoiment()
    {
        var pets =await _unitOfWork.Pets.GetPetsByAppoiment();
        var petsDto = _mapper.Map<IEnumerable<PetsAppoimentDto>>(pets);

        return Ok(petsDto);
    }
    [HttpGet("petsByAppoiment")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsAppoimentDto>>> GetPagination4([FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetPetsByAppoimentP(p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsAppoimentDto>>(pets.registros);
        return  new Pager<PetsAppoimentDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }
    //6.1Lista de las mascotas que fueron atendidas por motivo de vacunacion en cualquier trimestre del 2023
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
    [HttpGet("petsByAppoiment/{quarter}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsAppoimentDto>>> GetPagination5(int quarter,[FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetPetsByAppoimentP(quarter,p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsAppoimentDto>>(pets.registros);
        return  new Pager<PetsAppoimentDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
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
    [HttpGet("petsByVeterinarian/{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsAppoimentDto>>> GetPagination6(int id,[FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetPetsByVeterinarianP(id,p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsAppoimentDto>>(pets.registros);
        return  new Pager<PetsAppoimentDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    //11.Lista de las mascotas y sus propietarios cuya raza sea Golden Retriver
    [HttpGet("ownerPetsByBreed")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetOwnerPetsByBreed()
    {
        var pets =await _unitOfWork.Pets.GetOwnerPetsByBreed();
        var petsDto = _mapper.Map<IEnumerable<PetsOwnerDto>>(pets);

        return Ok(petsDto);
    }
    [HttpGet("ownerPetsByBreed")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsOwnerDto>>> GetPagination7([FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetOwnerPetsByBreedP(p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsOwnerDto>>(pets.registros);
        return  new Pager<PetsOwnerDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }
    //11.1 Lista de las mascotas y sus propietarios según una raza
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
    [HttpGet("ownerPetsByBreed/{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsOwnerDto>>> GetPagination8(int id, [FromQuery] Params p)
    {
        var pets = await _unitOfWork.Pets.GetOwnerPetsByBreedP(id, p.PageIndex, p.PageSize, p.Search);
        var petsDto = _mapper.Map<List<PetsOwnerDto>>(pets.registros);
        return  new Pager<PetsOwnerDto>(petsDto,pets.totalRegistros, p.PageIndex, p.PageSize, p.Search);
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
