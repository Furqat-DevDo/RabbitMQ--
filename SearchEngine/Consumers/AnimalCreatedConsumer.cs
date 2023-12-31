using AutoMapper;
using Events.Animal;
using MassTransit;
using MongoDB.Entities;
using SearchEngine.Data;

namespace SearchEngine.Consumers;

public class AnimalCreatedConsumer : IConsumer<AnimalCreated>
{
    private readonly IMapper _mapper;

    public AnimalCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AnimalCreated> animalCreated)
    {
        Console.WriteLine("Consuming animal created " + animalCreated.Message.Id);

        var animal = _mapper.Map<Animal>(animalCreated.Message);

        await animal.SaveAsync();
    }
}