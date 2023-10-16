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

public class ReasonController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReasonController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ReasonDto>>> Get()
    {
        var reasons = await _unitOfWork.Reasons.GetAllAsync();
        return _mapper.Map<List<ReasonDto>>(reasons);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Reason>> Post([FromBody] ReasonDto ReasonDto)
    {
        var reason = _mapper.Map<Reason>(ReasonDto);
        this._unitOfWork.Reasons.Add(reason);
        await _unitOfWork.SaveAsync();

        if(reason == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=reason.Id}, reason);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Reason>> put(ReasonDto ReasonDto)
    {
        if(ReasonDto == null){ return NotFound(); }
        var reason = this._mapper.Map<Reason>(ReasonDto);
        this._unitOfWork.Reasons.Update(reason);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return reason;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var reason = await _unitOfWork.Reasons.GetByIdAsync(id);
        if(reason == null)
        {
            return NotFound();
        }
        this._unitOfWork.Reasons.Remove(reason);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
