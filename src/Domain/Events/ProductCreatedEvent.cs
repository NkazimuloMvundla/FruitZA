using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitZA.Domain.Events;
public class ProductCreatedEvent: BaseEvent
{
    public ProductCreatedEvent(Product item)
    {
        Item = item;
    }

    public Product Item { get; }
}
