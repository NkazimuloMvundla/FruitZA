using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Domain.Events;
using Microsoft.Extensions.Logging;

namespace FruitZA.Application.TodoItems.EventHandlers;
/*internal class ProductCreatedEvent
{
}*/
public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FruitZA Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
