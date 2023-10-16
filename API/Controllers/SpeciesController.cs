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

public class SpeciesController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpeciesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<SpeciesDto>>> Get()
    {
        var speciesp = await _unitOfWork.SpeciesP.GetAllAsync();
        return _mapper.Map<List<SpeciesDto>>(speciesp);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Species>> Post([FromBody] SpeciesDto SpeciesDto)
    {
        var species = _mapper.Map<Species>(SpeciesDto);
        this._unitOfWork.SpeciesP.Add(species);
        await _unitOfWork.SaveAsync();

        if(species == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=species.Id}, species);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Species>> put(SpeciesDto SpeciesDto)
    {
        if(SpeciesDto == null){ return NotFound(); }
        var species = this._mapper.Map<Species>(SpeciesDto);
        this._unitOfWork.SpeciesP.Update(species);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return species;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var species = await _unitOfWork.SpeciesP.GetByIdAsync(id);
        if(species == null)
        {
            return NotFound();
        }
        this._unitOfWork.SpeciesP.Remove(species);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
