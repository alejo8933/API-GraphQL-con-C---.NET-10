using HotChocolate;
using HotChocolate.Subscriptions;

namespace AdventureGraphQL.Api.GraphQL;

public class Subscription
{
    [Subscribe]
    [Topic]
    public AddProductPayload OnProductAdded(
        [EventMessage] AddProductPayload product)
        => product;

    [Subscribe]
    [Topic]
    public UpdatePricePayload OnPriceChanged(
        [EventMessage] UpdatePricePayload payload)
        => payload;
}