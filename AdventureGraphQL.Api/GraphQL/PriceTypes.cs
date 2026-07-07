namespace AdventureGraphQL.Api.GraphQL;

public record UpdatePriceInput(
    int ProductId,
    decimal NewPrice);

public record UpdatePricePayload(
    int ProductId,
    string Name,
    decimal OldPrice,
    decimal NewPrice);