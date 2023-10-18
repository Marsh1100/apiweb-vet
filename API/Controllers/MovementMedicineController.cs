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

public class MovementMedicineController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovementMedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovementMedicineDto>>> Get()
    {
        var mov = await _unitOfWork.MovementMedicines.GetAllAsync();
        return _mapper.Map<List<MovementMedicineDto>>(mov);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MovementMedicineAllDto>>> GetPagination([FromQuery] Params p)
    {
        var mov = await _unitOfWork.MovementMedicines.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var movDto = _mapper.Map<List<MovementMedicineAllDto>>(mov.registros);
        return  new Pager<MovementMedicineAllDto>(movDto,mov.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] MovementMedicineDto movDto)
    {
        var mov = _mapper.Map<MovementMedicine>(movDto);
        var result =await _unitOfWork.MovementMedicines.RegisterAsync(mov);

        return Ok(result);
    }

    

}
