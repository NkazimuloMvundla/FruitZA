using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Domain.Entities;

namespace FruitZA.Application.Products.Queries.CreateProduct;
public class ProductDto
{
    public int Id { get; set; }
    public required string ProductCode { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string CategoryName { get; set; }
    public decimal Price { get; set; }
    public byte[]? Image { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}

