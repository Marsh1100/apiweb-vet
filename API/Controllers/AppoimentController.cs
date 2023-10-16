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

    public async Task<ActionResult<IEnumerable<AppoimentDto>>> Get()
    {
        var appoiments = await _unitOfWork.Appoiments.GetAllAsync();
        return _mapper.Map<List<AppoimentDto>>(appoiments);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] AppoimentDto appoimentDto)
    {
        var appoiment = _mapper.Map<Appoiment>(appoimentDto);
        var result =await _unitOfWork.Appoiments.RegisterAsync(appoiment);

        return Ok(result);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Appoiment>> put(AppoimentDto appoimentDto)
    {
        if(appoimentDto == null){ return NotFound(); }
        var appoiment = this._mapper.Map<Appoiment>(appoimentDto);
        this._unitOfWork.Appoiments.Update(appoiment);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return appoiment;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var appoiment = await _unitOfWork.Appoiments.GetByIdAsync(id);
        if(appoiment == null)
        {
            return NotFound();
        }
        this._unitOfWork.Appoiments.Remove(appoiment);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
