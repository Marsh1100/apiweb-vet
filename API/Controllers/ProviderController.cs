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

public class ProviderController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProviderController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProviderDto>>> Get()
    {
        var providers = await _unitOfWork.Providers.GetAllAsync();
        return _mapper.Map<List<ProviderDto>>(providers);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Provider>> Post([FromBody] ProviderDto ProviderDto)
    {
        var provider = _mapper.Map<Provider>(ProviderDto);
        this._unitOfWork.Providers.Add(provider);
        await _unitOfWork.SaveAsync();

        if(provider == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=provider.Id}, provider);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Provider>> put(ProviderPutDto providerPutDto)
    {
        if(providerPutDto == null){ return NotFound(); }
        var provider = this._mapper.Map<Provider>(providerPutDto);
        this._unitOfWork.Providers.Update(provider);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return provider;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var provider = await _unitOfWork.Providers.GetByIdAsync(id);
        if(provider == null)
        {
            return NotFound();
        }
        this._unitOfWork.Providers.Remove(provider);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

}
