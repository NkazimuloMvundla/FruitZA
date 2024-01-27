using FruitZA.Application.Products.Queries.GetProducts;
using FruitZA.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace FruitZA.Web.Endpoints;
public class WeatherForecasts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetWeatherForecasts)
            .MapGet(GetProducts,"GetProducts");
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }

    public async Task<ProductsVm> GetProducts(ISender sender)
    {
        return await sender.Send(new GetProductsQuery());
    }
}
