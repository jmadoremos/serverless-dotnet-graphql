namespace GraphQL;

using GraphQL.Data;
using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;
using GraphQL.Repositories.Database.Tracks;
using GraphQL.Repositories.StarWars.Characters;
using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Repositories.StarWars.Species;
using GraphQL.Repositories.StarWars.Starships;
using GraphQL.Repositories.StarWars.Vehicles;
using GraphQL.Schemas.Database.Attendees;
using GraphQL.Schemas.Database.Sessions;
using GraphQL.Schemas.Database.Speakers;
using GraphQL.Schemas.Database.Tracks;
using GraphQL.Schemas.StarWars.Characters;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;
using GraphQL.Schemas.StarWars.Species;
using GraphQL.Schemas.StarWars.Starships;
using GraphQL.Schemas.StarWars.Vehicles;
using GraphQL.Services.StarWars;
using Microsoft.EntityFrameworkCore;

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

        // Allow access to DbContext of Entity Framework
        const string dbHost = "localhost";
        const string dbPort = "5432";
        const string dbName = "GraphQL";
        const string dbUser = "postgre";
        const string dbPass = "postgre";
        const string dbconn = $"User ID={dbUser};Password={dbPass};Host={dbHost};Port={dbPort};Database={dbName};Pooling=true;Connection Lifetime=0;";
        services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
            options.UseNpgsql(dbconn));

        // Define GraphQL server parameters
        services.AddGraphQLServer()
            // DbContext
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)
            // Custom services using DbContext
            .RegisterService<AttendeeRepository>()
            .RegisterService<SessionRepository>()
            .RegisterService<SpeakerRepository>()
            .RegisterService<TrackRepository>()
            // Queries
            .AddQueryType(d => d.Name("Query"))
                // Database
                .AddType<AttendeeQuery>()
                .AddType<SessionQuery>()
                .AddType<SpeakerQuery>()
                .AddType<TrackQuery>()
                // Web API
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
            // Mutations
            .AddMutationType(d => d.Name("Mutation"))
                .AddType<AttendeeMutation>()
                .AddType<SessionMutation>()
                .AddType<SpeakerMutation>()
                .AddType<TrackMutation>();

        // Allow dependency injection of testable custom services
        services.AddSingleton<ISwapiService, SwapiService>();

        // Allow dependency injection of testable custom repositories
        services
            // Database
            .AddTransient<IAttendeeRepository, AttendeeRepository>()
            .AddTransient<ISessionRepository, SessionRepository>()
            .AddTransient<ISpeakerRepository, SpeakerRepository>()
            .AddTransient<ITrackRepository, TrackRepository>()
            // Web API
            .AddSingleton<ICharacterRepository, CharacterRepository>()
            .AddSingleton<IFilmRepository, FilmRepository>()
            .AddSingleton<IPlanetRepository, PlanetRepository>()
            .AddSingleton<ISpeciesRepository, SpeciesRepository>()
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
