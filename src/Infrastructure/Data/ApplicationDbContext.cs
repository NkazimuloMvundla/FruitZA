using System.Reflection;
using FruitZA.Application.Common.Interfaces;
using FruitZA.Domain.Entities;
using FruitZA.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FruitZA.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<AuditAction> AuditAction => Set<AuditAction>();

    public DbSet<AuditActionLog> AuditActionLog => Set<AuditActionLog>();

    public DbSet<AuditItem> AuditItem => Set<AuditItem>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
