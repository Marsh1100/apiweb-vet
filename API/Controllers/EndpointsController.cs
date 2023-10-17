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

public class EndpointsController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EndpointsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //1. Visualización de los veterinarios cuya especialidad sea Cirugía vascular.
    [HttpGet("veterinariansBySpeciality/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetVeterinariansBySpecialty(int id)
    {
        var veterinarians =await _unitOfWork.Veterinarians.GetVeterinariansBySpecialty(id);
        var veterinariansDto = _mapper.Map<IEnumerable<VetSpecialityDto>>(veterinarians);

        return Ok(veterinariansDto);
    }

    //2. Lista de los medicamentos que pertenezcan a el laboratorio Genfar.
    [HttpGet("medicinesByLaboratory/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetMedicinesByLaboratory(int id)
    {
        var medicines =await _unitOfWork.Medicines.GetMedicinesByLaboratory(id);
        var medicinesDto = _mapper.Map<IEnumerable<MedicineBaseDto>>(medicines);

        return Ok(medicinesDto);
    }

    //3. Mascotas que se encuentren registradas cuya especie sea felina.
    [HttpGet("petBySpecie/{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetPetBySpecie(int id)
    {
        var pets =await _unitOfWork.Pets.GetPetBySpecie(id);
        var petsDto = _mapper.Map<IEnumerable<PetsOnlyDto>>(pets);

        return Ok(petsDto);
    }

    //4.Lista de los propietarios y sus mascotas.
    [HttpGet("ownerPets")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> GetOwnerPets()
    {
        var pets =await _unitOfWork.Owners.GetOwnerPets();
        var petsDto = _mapper.Map<IEnumerable<OwnerPetsDto>>(pets);

        return Ok(petsDto);
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
