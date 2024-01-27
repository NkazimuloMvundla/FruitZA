using FruitZA.Application.Products.Commands.CreateProduct;
using FruitZA.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.AspNetCore.Mvc;

namespace FruitZA.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapPost(CreateProduct);
    }

    public async Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return await sender.Send(command);
    }
}
