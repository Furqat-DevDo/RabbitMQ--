using AutoMapper;
using Events.Animal;
using SearchEngine.Data;

namespace SearchEngine.Helpers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {

        CreateMap<AnimalCreated, Animal>();
        CreateMap<AnimalUpdated, Animal>();
    }
}