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

public class OwnerController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OwnerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
    {
        var owners = await _unitOfWork.Owners.GetAllAsync();
        return _mapper.Map<List<OwnerDto>>(owners);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<OwnerDto>>> GetPagination([FromQuery] Params p)
    {
        var owners = await _unitOfWork.Owners.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var ownersDto = _mapper.Map<List<OwnerDto>>(owners.registros);
        return  new Pager<OwnerDto>(ownersDto,owners.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Owner>> Post([FromBody] OwnerDto ownerDto)
    {
        var owner = _mapper.Map<Owner>(ownerDto);
        this._unitOfWork.Owners.Add(owner);
        await _unitOfWork.SaveAsync();

        if(owner == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=owner.Id}, owner);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Owner>> put(OwnerDto OwnerDto)
    {
        if(OwnerDto == null){ return NotFound(); }
        var owner = this._mapper.Map<Owner>(OwnerDto);
        this._unitOfWork.Owners.Update(owner);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return owner;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var owner = await _unitOfWork.Owners.GetByIdAsync(id);
        if(owner == null)
        {
            return NotFound();
        }
        this._unitOfWork.Owners.Remove(owner);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //4.Lista de los propietarios y sus mascotas.
    [HttpGet("ownerPets")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetOwnerPets()
    {
        var pets =await _unitOfWork.Owners.GetOwnerPets();
        var petsDto = _mapper.Map<IEnumerable<OwnerPetsDto>>(pets);

        return Ok(petsDto);
    }
    [HttpGet("ownerPets")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<OwnerPetsDto>>> GetPagination2([FromQuery] Params p)
    {
        var owners = await _unitOfWork.Owners.GetOwnerPetsP(p.PageIndex, p.PageSize, p.Search);
        var ownersDto = _mapper.Map<List<OwnerPetsDto>>(owners.registros);
        return  new Pager<OwnerPetsDto>(ownersDto,owners.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

}
