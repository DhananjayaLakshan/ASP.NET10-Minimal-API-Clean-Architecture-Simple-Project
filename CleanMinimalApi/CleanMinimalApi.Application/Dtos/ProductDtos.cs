namespace CleanMinimalApi.Application.Dtos;

public sealed record ProductResponse(Guid Id, string Name, decimal Price, DateTime CreatedAtUtc);
public sealed record CreateProductRequest(string Name, decimal Price);
public sealed record UpdateProductRequest(string Name, decimal Price);
