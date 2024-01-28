using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Models;
using FruitZA.Application.Products.Queries.CreateProduct;


namespace FruitZA.Application.Products.Queries.GetProducts;
public class ProductsVm
{
/*    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();
*/
    public IReadOnlyCollection<ProductDto> Products { get; init; } = Array.Empty<ProductDto>();
}
