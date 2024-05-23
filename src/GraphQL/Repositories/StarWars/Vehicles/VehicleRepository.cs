namespace GraphQL.Repositories.StarWars.Vehicles;

using System.Globalization;
using GraphQL.Services.StarWars;

public class VehicleRepository([Service] ISwapiService swapi) : IVehicleRepository
{
    private readonly Uri baseUri = new("https://swapi.dev/api/vehicles/");

    public async Task<SwapiResponseList<VehicleApiResponse>> GetAllAsync(CancellationToken ctx)
    {
        // Define placeholder of results
        var result = new SwapiResponseList<VehicleApiResponse>();

        // Define placeholder for the API response
        SwapiResponseList<VehicleApiResponse>? response;

        // Define API URI to call. Defaults to resource URI.
        var uri = this.baseUri.ToString();

        do
        {
            // Call API
            response = await swapi
                .GetAsync<SwapiResponseList<VehicleApiResponse>>(uri, ctx);

            // If the response does not have a data, return the result
            if (response == null || response.Count == 0)
            {
                break;
            }

            // Since the response has data, merge it to result
            result.Count += response.Count;
            foreach (var e in response.Results)
            {
                result.Results = result.Results.Append(e);
            }

            // Define the next URI to call based on the "next" property of the response
            uri = response.Next;
        } while (uri is not null);

        result.Previous = string.Empty;
        result.Next = string.Empty;

        // Resolve
        return result;
    }

    public async Task<VehicleApiResponse?> GetByIdAsync(
        int id,
        CancellationToken ctx)
    {
        // Define API URI to call
        var uri = new Uri(this.baseUri, Convert.ToString(id, CultureInfo.InvariantCulture));

        // Call API
        var result = await swapi
            .GetAsync<VehicleApiResponse>(uri, ctx);

        // Resolve
        return result;
    }
}
