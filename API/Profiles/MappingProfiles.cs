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
            .ForMember(dest=> dest.Speciality, origen=>  origen.MapFrom(origen => origen.Speciality.Name))
            .ReverseMap();
        CreateMap<Medicine, MedicineBaseDto>()
            .ForMember(dest => dest.Laboratory, origen=> origen.MapFrom(origen => origen.Laboratory.Name))
            .ReverseMap();

        CreateMap<Pet, PetsOnlyDto>()
            .ForMember(dest => dest.Breed, origen=> origen.MapFrom(origen => origen.Breed.Name))
            .ForMember(dest => dest.Species, origen=> origen.MapFrom(origen => origen.Breed.Species.Name))
            .ForMember(dest => dest.Owner, origen=> origen.MapFrom(origen => origen.Owner.Name))
            .ReverseMap();
        CreateMap<Owner, OwnerPetsDto>()
            .ForMember(dest => dest.Pets, origen=> origen.MapFrom(origen => origen.Pets))
            .ReverseMap();
        
        CreateMap<Pet, PetsOwnerDto>()
            .ForMember(dest => dest.Breed, origen=> origen.MapFrom(origen => origen.Breed.Name))
            .ForMember(dest => dest.Species, origen=> origen.MapFrom(origen => origen.Breed.Species.Name))
            .ReverseMap();
        CreateMap<Appoiment, PetsAppoimentDto>()
            .ForMember(dest => dest.Id, origen=> origen.MapFrom(origen => origen.Pet.Id))
            .ForMember(dest => dest.Veterinarian, origen=> origen.MapFrom(origen => origen.Vet.Name))
            .ForMember(dest => dest.Name, origen=> origen.MapFrom(origen => origen.Pet.Name))
            .ForMember(dest => dest.Breed, origen=> origen.MapFrom(origen => origen.Pet.Breed.Name))
            .ForMember(dest => dest.Birthdate, origen=> origen.MapFrom(origen => origen.Pet.Birthdate))
            .ForMember(dest => dest.Reason, origen=> origen.MapFrom(origen => origen.Reason.Name))

            .ForMember(dest => dest.DateAppoiment, origen=> origen.MapFrom(origen => origen.Date))
            .ReverseMap();
        
        CreateMap<Provider, ProviderAllDto>()
            .ForMember(dest => dest.Medicines, origen=> origen.MapFrom(origen => origen.Medicines))
            .ReverseMap();
        CreateMap<Vet,VetAllDto>()
            .ForMember(dest=> dest.Speciality, origen=> origen.MapFrom(origen => origen.Speciality.Name))
            .ReverseMap();
        CreateMap<Breed, BreedDto>()
            .ReverseMap();
        CreateMap<MovementMedicine, MovementMedicineAllDto>()
            .ForMember(dest => dest.Medicine, origen=> origen.MapFrom(origen => origen.Medicine.Name))
            .ForMember(dest => dest.MovementType, origen=> origen.MapFrom(origen => origen.MovementType.Name))
            .ReverseMap();
        CreateMap<Appoiment, AppoimentAllDto>()
            .ForMember(dest => dest.Vet, origen=> origen.MapFrom(origen => origen.Vet.Name))
            .ForMember(dest => dest.Reason, origen=> origen.MapFrom(origen => origen.Reason.Name))
            .ForMember(dest => dest.Pet, origen=> origen.MapFrom(origen => origen.Pet.Name))
            .ReverseMap();
        CreateMap<User, UserDto>()
            .ReverseMap();


    }   
}
