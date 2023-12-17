using AutoMapper;
using Events.Animal;
using MassTransit;
using MongoDB.Entities;
using SearchEngine.Data;

namespace SearchEngine.Consumers;

public class AnimalUpdatedConsumer : IConsumer<AnimalUpdated>
{
    private readonly IMapper _mapper;

    public AnimalUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AnimalUpdated> context)
    {
        Console.WriteLine("Consuming animal update " + context.Message.Id);

        var animal = _mapper.Map<Animal>(context.Message);

        var result = await DB.Update<Animal>().Match(a => a.ID == context.Message.Id).ModifyOnly(
            updatedAnimal => new
            {
                updatedAnimal.Name,
                updatedAnimal.Age,
                updatedAnimal.Description,
                updatedAnimal.Breed,
                updatedAnimal.Sex,
                updatedAnimal.Weight,
                updatedAnimal.Color,
                updatedAnimal.Type,
                updatedAnimal.CoverImageUrl,
                updatedAnimal.UpdatedAt,

            }, animal).ExecuteAsync();
        
        if (!result.IsAcknowledged)
            throw new MessageException(typeof(AnimalUpdated), "Problem updating mongodb");
    }
}