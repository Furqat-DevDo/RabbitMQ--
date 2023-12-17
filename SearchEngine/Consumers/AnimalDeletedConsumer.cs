using Events.Animal;
using MassTransit;
using MongoDB.Entities;
using SearchEngine.Data;

namespace SearchEngine.Consumers;

public class AnimalDeletedConsumer : IConsumer<AnimalDeleted>
{
    public async  Task Consume(ConsumeContext<AnimalDeleted> context)
    {
        Console.WriteLine("Consuming animal delete " + context.Message.Id);

        var result = await DB.DeleteAsync<Animal>(context.Message.Id);

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(AnimalDeleted), "Problem deleting Course");
    }
}