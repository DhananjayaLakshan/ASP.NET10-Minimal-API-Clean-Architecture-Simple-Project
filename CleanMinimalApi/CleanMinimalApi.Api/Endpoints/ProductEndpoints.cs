using CleanMinimalApi.Application.Abstractions.Persistence;
using CleanMinimalApi.Application.Dtos;
using CleanMinimalApi.Domain.Entities;

namespace CleanMinimalApi.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");

        group.MapGet("/", async (IProductRepository repo, CancellationToken ct) =>
        {
            var products = await repo.GetAllAsync(ct);
            var response = products.Select(p => new ProductResponse(p.Id, p.Name, p.Price, p.CreatedAtUtc));
            return Results.Ok(response);
        });

        group.MapGet("/{id:guid}", async (Guid id, IProductRepository repo, CancellationToken ct) =>
        {
            var product = await repo.GetByIdAsync(id, ct);
            return product is null
                ? Results.NotFound()
                : Results.Ok(new ProductResponse(product.Id, product.Name, product.Price, product.CreatedAtUtc));
        });

        group.MapPost("/", async (CreateProductRequest req, IProductRepository repo, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(req.Name)) return Results.BadRequest("Name is required.");
            if (req.Price < 0) return Results.BadRequest("Price must be >= 0.");

            var product = new Product { Name = req.Name.Trim(), Price = req.Price };
            await repo.AddAsync(product, ct);

            return Results.Created($"/api/products/{product.Id}",
                new ProductResponse(product.Id, product.Name, product.Price, product.CreatedAtUtc));
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateProductRequest req, IProductRepository repo, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(req.Name)) return Results.BadRequest("Name is required.");
            if (req.Price < 0) return Results.BadRequest("Price must be >= 0.");

            var product = await repo.GetByIdAsync(id, ct);
            if (product is null) return Results.NotFound();

            product.Name = req.Name.Trim();
            product.Price = req.Price;

            await repo.UpdateAsync(product, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (Guid id, IProductRepository repo, CancellationToken ct) =>
        {
            var product = await repo.GetByIdAsync(id, ct);
            if (product is null) return Results.NotFound();

            await repo.DeleteAsync(product, ct);
            return Results.NoContent();
        });

        return app;
    }
}
