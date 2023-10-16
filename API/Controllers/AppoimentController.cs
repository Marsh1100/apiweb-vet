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

public class AppoimentController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppoimentController(IUnitOfWork unitOfWork, IMapper mapper)
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

}
