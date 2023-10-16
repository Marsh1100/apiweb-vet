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

public class MovementTypeController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovementTypeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MovementTypeDto>>> Get()
    {
        var mtypes = await _unitOfWork.MovementTypes.GetAllAsync();
        return _mapper.Map<List<MovementTypeDto>>(mtypes);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovementType>> Post([FromBody] MovementTypeDto MovementTypeDto)
    {
        var mtype = _mapper.Map<MovementType>(MovementTypeDto);
        this._unitOfWork.MovementTypes.Add(mtype);
        await _unitOfWork.SaveAsync();

        if(mtype == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=mtype.Id}, mtype);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MovementType>> put(MovementTypeDto MovementTypeDto)
    {
        if(MovementTypeDto == null){ return NotFound(); }
        var mtype = this._mapper.Map<MovementType>(MovementTypeDto);
        this._unitOfWork.MovementTypes.Update(mtype);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return mtype;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var mtype = await _unitOfWork.MovementTypes.GetByIdAsync(id);
        if(mtype == null)
        {
            return NotFound();
        }
        this._unitOfWork.MovementTypes.Remove(mtype);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
