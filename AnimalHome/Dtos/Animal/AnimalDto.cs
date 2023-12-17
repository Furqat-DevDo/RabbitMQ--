namespace AnimalHome.Dtos.Animal;

public record AnimalDto(
    Guid Id, 
    int PublicId,
    int Age,
    string Name,
    string Type,
    string Breed,
    string Sex ,
    int Weight ,
    string Color ,
    string Description ,
    string CoverImageUrl ,
    string Status ,
    DateTime CreatedAt ,
    DateTime UpdatedAt);