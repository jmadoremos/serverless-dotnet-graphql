namespace GraphQL.Gateway;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient(WellKnownSchemaNames.StarWars, c => c.BaseAddress = new Uri("http://localhost:5002/graphql"));
        services.AddHttpClient(WellKnownSchemaNames.Database, c => c.BaseAddress = new Uri("http://localhost:5004/graphql"));

        // Define GraphQL server parameters
        services.AddGraphQLServer()
            // Define remote schemas
            .AddRemoteSchema(WellKnownSchemaNames.StarWars)
            .AddRemoteSchema(WellKnownSchemaNames.Database);
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
