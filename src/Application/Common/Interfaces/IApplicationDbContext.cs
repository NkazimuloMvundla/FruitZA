using FruitZA.Domain.Entities;

namespace FruitZA.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Product> Products { get; }

    DbSet<AuditActionLog> AuditActionLog { get; }

/*    DbSet<AuditItem> AuditItem { get; }*/

    DbSet<Category> Categories { get; }

    DbSet<UploadedExcel> Resources { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
