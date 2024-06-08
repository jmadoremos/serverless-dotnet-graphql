namespace GraphQL.StarWars;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Schemas.Characters;
using GraphQL.StarWars.Schemas.Films;
using GraphQL.StarWars.Schemas.Planets;
using GraphQL.StarWars.Schemas.Species;
using GraphQL.StarWars.Schemas.Starships;
using GraphQL.StarWars.Schemas.Vehicles;
using GraphQL.StarWars.Services;
using HotChocolate.Types.Pagination;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        // Allow access to HttpClient service
        services.AddHttpClient();

        // Allow access to the HttpContext of the current request
        services.AddHttpContextAccessor();

        // Allow dependency injection of testable classes
        services
            // Services
            .AddSingleton<ISwapiService, SwapiService>()
            // Repositories
            .AddSingleton(typeof(IStarWarsRepository<>), typeof(StarWarsRepository<>));

        // Define GraphQL server parameters
        services.AddGraphQLServer()
            // Enable global object identification
            .AddGlobalObjectIdentification()
            // Set paging options
            .SetPagingOptions(new PagingOptions
            {
                IncludeTotalCount = true
            })
            // Queries
            .AddQueryType(d => d.Name("Query"))
                .AddType<CharacterQuery>()
                .AddType<FilmQuery>()
                .AddType<PlanetQuery>()
                .AddType<SpeciesQuery>()
                .AddType<StarshipQuery>()
                .AddType<VehicleQuery>()
            // Extensions
            .AddTypeExtension<CharacterExtension>()
            .AddTypeExtension<FilmExtension>()
            .AddTypeExtension<PlanetExtension>()
            .AddTypeExtension<SpeciesExtension>()
            .AddTypeExtension<StarshipExtension>()
            .AddTypeExtension<VehicleExtension>()
            // Initialize schema on startup without waiting for the first request
            .InitializeOnStartup();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();

        app.UseRouting();

        // app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL("/graphql");

            endpoints.MapGraphQLSchema("/graphql/schema");

            endpoints.MapGraphQLWebSocket("/graphql/ws");
        });
    }
}
