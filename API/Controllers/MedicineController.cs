using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class MedicineController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicineDto>>> Get()
    {
        var medicines = await _unitOfWork.Medicines.GetAllAsync();
        return _mapper.Map<List<MedicineDto>>(medicines);
    }
     [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicineBaseDto>>> GetPagination([FromQuery] Params p)
    {
        var medicines = await _unitOfWork.Medicines.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var medDto = _mapper.Map<List<MedicineBaseDto>>(medicines.registros);
        return  new Pager<MedicineBaseDto>(medDto,medicines.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }
    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicine>> Post([FromBody] MedicineDto medicineDto)
    {
        var medicine = _mapper.Map<Medicine>(medicineDto);
        this._unitOfWork.Medicines.Add(medicine);
        await _unitOfWork.SaveAsync();

        if(medicine == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new{id=medicine.Id}, medicine);
    }

    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Medicine>> put(MedicineDto medicineDto)
    {
        if(medicineDto == null){ return NotFound(); }
        var medicine = this._mapper.Map<Medicine>(medicineDto);
        this._unitOfWork.Medicines.Update(medicine);
        Console.WriteLine(await this._unitOfWork.SaveAsync());

        return medicine;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Delete(int id)
    {
        var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
        if(medicine == null)
        {
            return NotFound();
        }
        this._unitOfWork.Medicines.Remove(medicine);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //2. Lista de los medicamentos que pertenezcan a el laboratorio Genfar.
    [HttpGet("medicinesByLaboratory/{id}")]
    [MapToApiVersion("1.0")]
    [Authorize(Roles = "Employee, Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetMedicinesByLaboratory(int id)
    {
        var medicines =await _unitOfWork.Medicines.GetMedicinesByLaboratory(id);
        var medicinesDto = _mapper.Map<IEnumerable<MedicineBaseDto>>(medicines);

        return Ok(medicinesDto);
    }

    //5.Lista de los medicamentos que tenga un precio de venta mayor a 50000.
    [HttpGet("medicinesPrice/{price}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetMedicinesPrice(double price)
    {
        var medicine =await _unitOfWork.Medicines.GetMedicinesPrice(price);
        var medicineDto = _mapper.Map<IEnumerable<MedicinePriceDto>>(medicine);

        return Ok(medicineDto);
    }
}
