using CleanMinimalApi.Application.Abstractions.Persistence;
using CleanMinimalApi.Domain.Entities;
using CleanMinimalApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanMinimalApi.Infrastructure.Repositories;

public sealed class ProductRepository(AppDbContext db) : IProductRepository
{
    public Task<List<Product>> GetAllAsync(CancellationToken ct) =>
        db.Products.AsNoTracking().OrderByDescending(x => x.CreatedAtUtc).ToListAsync(ct);

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct) =>
        db.Products.FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task AddAsync(Product product, CancellationToken ct)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Product product, CancellationToken ct)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Product product, CancellationToken ct)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync(ct);
    }
}
