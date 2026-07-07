using AdventureGraphQL.Api.Data;
using AdventureGraphQL.Api.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AdventureWorksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorks")));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<AdventureGraphQL.Api.GraphQL.Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .RegisterDbContextFactory<AdventureWorksContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.MapGraphQL();

app.Run();