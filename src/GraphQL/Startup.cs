﻿namespace GraphQL;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Repositories.StarWars.Starships;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;
using GraphQL.Services.StarWars;

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

        // Define GraphQL server parameters
        services
            .AddGraphQLServer()
            // Queries
            .AddQueryType(d => d.Name("Query"))
                .AddType<FilmQuery>()
                .AddType<PersonQuery>()
                .AddType<PlanetQuery>()
                .AddType<StarshipQuery>()
                .AddType<VehicleQuery>()
            // Extensions
            .AddType<PersonExtension>()
            .AddType<StarshipExtension>()
            .AddType<VehicleExtension>();

        // Allow dependency injection of testable custom services
        services.AddSingleton<ISwapiService, SwapiService>();

        // Allow dependency injection of testable custom repositories
        services
            .AddSingleton<IFilmRepository, FilmRepository>()
            .AddSingleton<IPersonRepository, PersonRepository>()
            .AddSingleton<IPlanetRepository, PlanetRepository>()
            .AddSingleton<IStarshipRepository, StarshipRepository>()
            .AddSingleton<IVehicleRepository, VehicleRepository>();
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
