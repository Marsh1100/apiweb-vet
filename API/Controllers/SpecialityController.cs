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

public class SpecialityController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpecialityController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<SpecialityDto>>> Get()
    {
        var specialities = await _unitOfWork.Specialities.GetAllAsync();
        return _mapper.Map<List<SpecialityDto>>(specialities);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SpecialityDto>>> GetPagination([FromQuery] Params p)
    {
        var specialities = await _unitOfWork.Providers.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var specialitiesDto = _mapper.Map<List<SpecialityDto>>(specialities.registros);
        return  new Pager<SpecialityDto>(specialitiesDto,specialities.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Speciality>> Post([FromBody] SpecialityDto specialityDto)
    {
        var specialities = _mapper.Map<Speciality>(specialityDto);
        this._unitOfWork.Specialities.Add(specialities);
        await _unitOfWork.SaveAsync();

        if(specialities == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=specialities.Id}, specialities);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Speciality>> put(SpecialityDto specialityDto)
    {
        if(specialityDto == null){ return NotFound(); }
        var specialities = this._mapper.Map<Speciality>(specialityDto);
        this._unitOfWork.Specialities.Update(specialities);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return specialities;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var specialities = await _unitOfWork.Specialities.GetByIdAsync(id);
        if(specialities == null)
        {
            return NotFound();
        }
        this._unitOfWork.Specialities.Remove(specialities);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
