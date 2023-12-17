namespace AnimalHome.Dtos.Animal;

public record UpdateAnimalDto(int? Age,
string? Name,
string? Type,
string? Breed,
string? Sex,
int? Weight ,
string? Color ,
string? Description,
string? CoverImageUrl,
string? Status);