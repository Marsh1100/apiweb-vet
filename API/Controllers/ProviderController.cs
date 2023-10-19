using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using API.Services;
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
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ProviderController(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = userService;
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
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProviderAllDto>>> GetPagination([FromQuery] Params p)
    {
        var providers = await _unitOfWork.Providers.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var providerDto = _mapper.Map<List<ProviderAllDto>>(providers.registros);
        return  new Pager<ProviderAllDto>(providerDto,providers.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Provider>> Post([FromBody] ProviderDto providerDto)
    {
        var provider = _mapper.Map<Provider>(providerDto);
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

    [HttpPost("addMedicine")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PostMedicine([FromBody] AddMedicineProviderDto model)
    {
       
        var result =await _userService.AddMedicineProvider(model);

        return Ok(result);
    }

    //10.Lista de los proveedores que me venden un determinado medicamento.
    [HttpGet("providersByMedicine/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ProviderDto>> GetProvidersByMedicine(int id)
    {
        var provider =await _unitOfWork.Providers.GetProvidersByMedicine(id);
        var providerDto = _mapper.Map<IEnumerable<ProviderDto>>(provider);

        return Ok(providerDto);
    }
    [HttpGet("providersByMedicine/{id}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProviderDto>>> GetPagination(int id,[FromQuery] Params p)
    {
        var provider = await _unitOfWork.Providers.GetProvidersByMedicineP(id,p.PageIndex, p.PageSize, p.Search);
        var providerDto = _mapper.Map<List<ProviderDto>>(provider.registros);
        return  new Pager<ProviderDto>(providerDto,provider.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

}
