namespace Events.Animal;

public record AnimalUpdated(
    string Name, 
    string Id, 
    int Age, 
    string Type, 
    string Breed, 
    string Sex, 
    int Weight, 
    string Color, 
    string Description, 
    string CoverImageUrl, 
    string Status);