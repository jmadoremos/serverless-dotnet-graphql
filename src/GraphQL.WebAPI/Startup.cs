namespace GraphQL.WebAPI;

using GraphQL.WebAPI.Repositories;
using GraphQL.WebAPI.Schemas.Characters;
using GraphQL.WebAPI.Schemas.Films;
using GraphQL.WebAPI.Schemas.Planets;
using GraphQL.WebAPI.Schemas.Species;
using GraphQL.WebAPI.Schemas.Starships;
using GraphQL.WebAPI.Schemas.Vehicles;
using GraphQL.WebAPI.Services;
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
            .AddTypeExtension<VehicleExtension>();
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

        app.UseEndpoints(endpoints => endpoints.MapGraphQL());
    }
}
