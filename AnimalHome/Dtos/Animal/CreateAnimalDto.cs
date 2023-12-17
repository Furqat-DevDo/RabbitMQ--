using AnimalHome.Entities;

namespace AnimalHome.Dtos.Animal;

public record CreateAnimalDto(
    int Age,
    string Name,
    string Type,
    string Breed,
    string Sex,
    int Weight,
    string Color,
    string Description,
    string CoverImageUrl,
    Status Status );