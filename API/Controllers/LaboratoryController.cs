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

public class LaboratoryController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LaboratoryController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<LaboratoryDto>>> Get()
    {
        var labs = await _unitOfWork.Laboratories.GetAllAsync();
        return _mapper.Map<List<LaboratoryDto>>(labs);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Laboratory>> Post([FromBody] LaboratoryDto LaboratoryDto)
    {
        var lab = _mapper.Map<Laboratory>(LaboratoryDto);
        this._unitOfWork.Laboratories.Add(lab);
        await _unitOfWork.SaveAsync();

        if(lab == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=lab.Id}, lab);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Laboratory>> put(LaboratoryPutDto laboratoryPutDto)
    {
        if(laboratoryPutDto == null){ return NotFound(); }
        var lab = this._mapper.Map<Laboratory>(laboratoryPutDto);
        this._unitOfWork.Laboratories.Update(lab);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return lab;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var laboratory = await _unitOfWork.Laboratories.GetByIdAsync(id);
        if(laboratory == null)
        {
            return NotFound();
        }
        this._unitOfWork.Laboratories.Remove(laboratory);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
