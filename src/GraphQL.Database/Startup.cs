namespace GraphQL.Database;

using GraphQL.Database.DataLoaders;
using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.SessionAttendees;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.SessionSpeakers;
using GraphQL.Database.Repositories.Speakers;
using GraphQL.Database.Repositories.Tracks;
using GraphQL.Database.Schemas.Attendees;
using GraphQL.Database.Schemas.Sessions;
using GraphQL.Database.Schemas.Speakers;
using GraphQL.Database.Schemas.Tracks;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        // Allow access to the HttpContext of the current request
        services.AddHttpContextAccessor();

        // Allow access to DbContext of Entity Framework
        services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
            options.UseNpgsql(this.Configuration.GetConnectionString("GraphQL")));

        // Allow dependency injection of testable classes
        services
            // Database
            .AddTransient<IAttendeeRepository, AttendeeRepository>()
            .AddTransient<ISessionRepository, SessionRepository>()
            .AddTransient<ISessionAttendeeRepository, SessionAttendeeRepository>()
            .AddTransient<ISessionSpeakerRepository, SessionSpeakerRepository>()
            .AddTransient<ISpeakerRepository, SpeakerRepository>()
            .AddTransient<ITrackRepository, TrackRepository>();

        // Define GraphQL server parameters
        services.AddGraphQLServer()
            // DbContext
            .RegisterDbContext<ApplicationDbContext>(DbContextKind.Pooled)
            // Enable global object identification
            .AddGlobalObjectIdentification()
            // Set execution timeout
            //.ModifyRequestOptions(options => options.ExecutionTimeout = TimeSpan.FromSeconds(60))
            // Set paging options
            .SetPagingOptions(new PagingOptions
            {
                IncludeTotalCount = true
            })
            // Custom services
            .RegisterService<IAttendeeRepository>()
            .RegisterService<ISessionRepository>()
            .RegisterService<ISessionAttendeeRepository>()
            .RegisterService<ISessionSpeakerRepository>()
            .RegisterService<ISpeakerRepository>()
            .RegisterService<ITrackRepository>()
            // Data loaders
            .AddDataLoader<AttendeeByIdBatchDataLoader>()
            .AddDataLoader<SessionByIdBatchDataLoader>()
            .AddDataLoader<SpeakerByIdBatchDataLoader>()
            .AddDataLoader<TrackByIdBatchDataLoader>()
            // Queries
            .AddQueryType(d => d.Name("Query"))
                .AddType<AttendeeQuery>()
                .AddType<SessionQuery>()
                .AddType<SpeakerQuery>()
                .AddType<TrackQuery>()
            // Extensions
            .AddTypeExtension<AttendeeExtension>()
            .AddTypeExtension<SessionExtension>()
            .AddTypeExtension<SpeakerExtension>()
            .AddTypeExtension<TrackExtension>()
            // Mutations
            .AddMutationType(d => d.Name("Mutation"))
                .AddType<AttendeeMutation>()
                .AddType<SessionMutation>()
                .AddType<SpeakerMutation>()
                .AddType<TrackMutation>();
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
