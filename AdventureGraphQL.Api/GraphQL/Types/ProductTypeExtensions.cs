using AdventureGraphQL.Api.Data;
using AdventureGraphQL.Api.Data.Entities;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace AdventureGraphQL.Api.GraphQL.Types;

[ExtendObjectType(typeof(Product))]
public class ProductTypeExtensions
{
    public async Task<string?> GetCategory(
        [Parent] Product product,
        IDbContextFactory<AdventureWorksContext> factory,
        CancellationToken ct)
    {
        await using var context = factory.CreateDbContext();

        return await context.Products
            .Where(p => p.ProductID == product.ProductID)
            .Select(p => p.ProductSubcategory != null
                ? p.ProductSubcategory.ProductCategory.Name
                : null)
            .FirstOrDefaultAsync(ct);
    }
}