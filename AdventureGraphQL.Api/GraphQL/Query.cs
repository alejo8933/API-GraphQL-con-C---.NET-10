using AdventureGraphQL.Api.Data;
using AdventureGraphQL.Api.Data.Entities;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;

namespace AdventureGraphQL.Api.GraphQL;

public class Query
{
    [UsePaging(MaxPageSize = 50)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Product> GetProducts(IDbContextFactory<AdventureWorksContext> factory)
    {
        var ctx = factory.CreateDbContext();
        return ctx.Products;
    }

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Customer> GetCustomers(IDbContextFactory<AdventureWorksContext> factory)
    {
        var ctx = factory.CreateDbContext();
        return ctx.Customers;
    }

    [UseProjection]
    public IQueryable<Product> GetProductById(int id, IDbContextFactory<AdventureWorksContext> factory)
    {
        var ctx = factory.CreateDbContext();
        return ctx.Products.Where(p => p.ProductID == id);
    }

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductCatalogItem> GetProductCatalog(IDbContextFactory<AdventureWorksContext> factory)
    {
        var ctx = factory.CreateDbContext();

        return ctx.Products.Select(p => new ProductCatalogItem
        {
            ProductID = p.ProductID,
            Name = p.Name,
            ListPrice = p.ListPrice,
            Category = p.ProductSubcategory != null
                ? p.ProductSubcategory.ProductCategory.Name
                : null
        });
    }
}

public class ProductCatalogItem
{
    public int ProductID { get; set; }
    public string Name { get; set; } = null!;
    public decimal ListPrice { get; set; }
    public string? Category { get; set; }
}