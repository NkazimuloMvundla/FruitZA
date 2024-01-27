using AutoMapper;
using FruitZA.Domain.Entities;

namespace FruitZA.Application.Common.Models
{
    public class ProductPaginationDto
    {
        public int Id { get; init; }

        public required string ProductCode { get; init; }

        public required string Name { get; init; }

        public required string Description { get; init; }

        public required string CategoryName { get; init; }

        public decimal Price { get; init; }

        public byte[]? Image { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Product, ProductPaginationDto>();
            }
        }
    }
}
