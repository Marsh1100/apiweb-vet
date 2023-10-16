using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
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

    public async Task<ActionResult<Pet>> put(PetDto petDto)
    {
        if(petDto == null){ return NotFound(); }
        var pet = this._mapper.Map<Pet>(petDto);
        this._unitOfWork.Pets.Update(pet);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return pet;
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

}
