using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
   public MappingProfiles()
    {
        CreateMap<Owner,OwnerDto>()
            .ReverseMap()
            .ForMember(origen => origen.Pets, dest=> dest.Ignore()); 
        CreateMap<Reason,ReasonDto>()
            .ReverseMap()
            .ForMember(origen => origen.Appoiments, dest=> dest.Ignore());  
        CreateMap<Laboratory,LaboratoryDto>()
            .ReverseMap();
        CreateMap<Laboratory,LaboratoryPutDto>()
            .ReverseMap();
        CreateMap<Provider,ProviderDto>()
            .ReverseMap();
        CreateMap<Provider,ProviderPutDto>()
            .ReverseMap();
        CreateMap<Speciality,SpecialityDto>()
            .ReverseMap();
        CreateMap<Species,SpeciesDto>()
            .ReverseMap();
        CreateMap<MovementType,MovementTypeDto>()
            .ReverseMap();

        CreateMap<Appoiment,AppoimentDto>()
            .ReverseMap();
        CreateMap<Vet, VetDto>()
            .ReverseMap();
        CreateMap<Pet, PetDto>()
            .ReverseMap();
        CreateMap<Medicine, MedicineDto>()
            .ReverseMap();
        CreateMap<MovementMedicine, MovementMedicineDto>()
            .ReverseMap();

        CreateMap<Vet, VetSpecialityDto>()
            .ReverseMap();
        CreateMap<Medicine, MedicineBaseDto>()
            .ReverseMap();

        CreateMap<Pet, PetsOnlyDto>()
            .ForMember(dest => dest.Breed, origen=> origen.MapFrom(origen => origen.Breed.Name))
            .ReverseMap();
        CreateMap<Owner, OwnerPetsDto>()
            .ForMember(dest => dest.Pets, origen=> origen.MapFrom(origen => origen.Pets))
            .ReverseMap();
        CreateMap<Medicine, MedicinePriceDto>()
            .ForMember(dest => dest.Laboratory, origen=> origen.MapFrom(origen => origen.Laboratory.Name))
            .ReverseMap();
        CreateMap<Pet, PetsOwnerDto>()
            .ForMember(dest => dest.Breed, origen=> origen.MapFrom(origen => origen.Breed.Name))
            .ForMember(dest => dest.Species, origen=> origen.MapFrom(origen => origen.Breed.Species.Name))
            .ReverseMap();

    }   
}
