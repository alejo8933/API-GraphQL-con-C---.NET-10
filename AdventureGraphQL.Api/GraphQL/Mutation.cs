using AdventureGraphQL.Api.Data;
using AdventureGraphQL.Api.Data.Entities;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace AdventureGraphQL.Api.GraphQL;

public record AddProductInput(
    string Name,
    string ProductNumber,
    decimal ListPrice,
    int? ProductSubcategoryID);

public record AddProductPayload(
    int ProductID,
    string Name,
    decimal ListPrice);

public class Mutation
{
    public async Task<AddProductPayload> AddProductAsync(
        AddProductInput input,
        AdventureWorksContext context,
        [Service] ITopicEventSender sender,
        CancellationToken ct)
    {
        if (input.ListPrice < 0)
        {
            throw new GraphQLException("El precio no puede ser negativo.");
        }

        var product = new Product
        {
            Name = input.Name,
            ProductNumber = input.ProductNumber,
            ListPrice = input.ListPrice,
            ProductSubcategoryID = input.ProductSubcategoryID,
            SellStartDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow,
            rowguid = Guid.NewGuid(),
            MakeFlag = true,
            FinishedGoodsFlag = true,
            SafetyStockLevel = 100,
            ReorderPoint = 10,
            StandardCost = 0,
            DaysToManufacture = 0
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(ct);

        var payload = new AddProductPayload(
            product.ProductID,
            product.Name,
            product.ListPrice);

        await sender.SendAsync(nameof(Subscription.OnProductAdded), payload, ct);

        return payload;
    }

    public async Task<UpdatePricePayload> UpdatePriceAsync(
        UpdatePriceInput input,
        AdventureWorksContext context,
        [Service] ITopicEventSender sender,
        CancellationToken ct)
    {
        var product = await context.Products.FindAsync(new object[] { input.ProductId }, ct);

        if (product is null)
        {
            throw new GraphQLException($"No existe el producto con id {input.ProductId}.");
        }

        if (input.NewPrice < 0)
        {
            throw new GraphQLException("El nuevo precio no puede ser negativo.");
        }

        var oldPrice = product.ListPrice;

        product.ListPrice = input.NewPrice;
        product.ModifiedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(ct);

        var payload = new UpdatePricePayload(
            product.ProductID,
            product.Name,
            oldPrice,
            product.ListPrice);

        await sender.SendAsync(nameof(Subscription.OnPriceChanged), payload, ct);

        return payload;
    }
}