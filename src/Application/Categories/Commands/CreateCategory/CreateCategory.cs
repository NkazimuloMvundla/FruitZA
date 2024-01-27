using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Domain.Entities;

namespace FruitZA.Application.Categories.Commands.CreateCategory;
public record CreateOrUpdateCategoryCommand : IRequest<int>
{
    public int Id { get; init; } // Used for update
    public required string CategoryCode { get; init; }
    public required string Name { get; init; }
}
public class CreateOrUpdateCategoryCommandValidator : AbstractValidator<CreateOrUpdateCategoryCommand>
{
    public CreateOrUpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryCode).NotEmpty().Matches(@"^[A-Za-z]{3}\d{3}$").WithMessage("Category code must have 3 alphabet letters followed by 3 numeric characters.");
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateOrUpdateCategoryCommandHandler : IRequestHandler<CreateOrUpdateCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOrUpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrUpdateCategoryCommand request, CancellationToken cancellationToken)
    {

        // Check if the category code already exists
        var categoryWithSameCode = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryCode == request.CategoryCode);

        if (request.Id != default(int))
        {
            // Update existing category
            var existingCategory = await _context.Categories.FindAsync(request.Id);

            if (existingCategory == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryCode);
            }

            // Check if the category code is being updated to an existing one
            if (categoryWithSameCode != null && categoryWithSameCode.Id != existingCategory.Id)
            {
                throw new Exception("Category code already exists.");
            }

            existingCategory.CategoryCode = request.CategoryCode;
            existingCategory.Name = request.Name;
            // Update other properties as needed

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync(cancellationToken);

            return existingCategory.Id;
        }
        else
        {
            // Create new category
            if (categoryWithSameCode != null)
            {
                throw new Exception("Category code already exists.");
            }

            // Check if the name is already associated with a different category
            var categoryWithSameName = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.Name);

            if (categoryWithSameName != null)
            {
                throw new Exception("Category name already exists for another category.");
            }

            var category = new Category
            {
                CategoryCode = request.CategoryCode,
                Name = request.Name
                // Set other properties as needed
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }

    }
}

