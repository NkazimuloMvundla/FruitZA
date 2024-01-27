using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Domain.Entities;

namespace FruitZA.Application.Categories.Queries.GetCategories;

public class CategoryDto
{
    public int Id { get; set; }
    public required string CategoryCode { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
