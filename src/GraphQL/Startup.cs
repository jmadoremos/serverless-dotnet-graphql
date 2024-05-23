namespace GraphQL;

using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Services.StarWars;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.People;
using GraphQL.Schemas.StarWars.Planets;

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
            // Extensions
            .AddType<PersonExtension>();

        // Allow dependency injection of testable custom services
        services.AddSingleton<ISwapiService, SwapiService>();

        // Allow dependency injection of testable custom repositories
        services
            .AddSingleton<IFilmRepository, FilmRepository>()
            .AddSingleton<IPersonRepository, PersonRepository>()
            .AddSingleton<IPlanetRepository, PlanetRepository>();
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
