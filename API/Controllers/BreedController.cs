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

public class BreedController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BreedController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<BreedDto>>> Get()
    {
        var breeds = await _unitOfWork.Breeds.GetAllAsync();
        return _mapper.Map<List<BreedDto>>(breeds);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<BreedDto>>> GetPagination([FromQuery] Params p)
    {
        var breed = await _unitOfWork.Breeds.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var breedDto = _mapper.Map<List<BreedDto>>(breed.registros);
        return  new Pager<BreedDto>(breedDto,breed.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Breed>> Post([FromBody] BreedDto breedDto)
    {
        var breed = _mapper.Map<Breed>(breedDto);
        this._unitOfWork.Breeds.Add(breed);
        await _unitOfWork.SaveAsync();

        if(breed == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=breed.Id}, breed);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Breed>> put(BreedDto breedDto)
    {
        if(breedDto == null){ return NotFound(); }
        var breed = this._mapper.Map<Breed>(breedDto);
        this._unitOfWork.Breeds.Update(breed);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return breed;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var breed = await _unitOfWork.Breeds.GetByIdAsync(id);
        if(breed == null)
        {
            return NotFound();
        }
        this._unitOfWork.Breeds.Remove(breed);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
