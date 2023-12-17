using AnimalHome.Dtos.Animal;
using AnimalHome.Entities;
using AutoMapper;
using Events.Animal;

namespace AnimalHome.Helpers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<Animal, AnimalDto>();
        CreateMap<CreateAnimalDto, Animal>();
        CreateMap<AnimalDto, AnimalCreated>();
        CreateMap<Animal, AnimalUpdated>();
    }
}