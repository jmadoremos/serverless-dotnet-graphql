namespace GraphQL.StarWars.Extensions;

using System.Globalization;

public static class UriExtension
{
    public static int ExtractSwapiId(this Uri @this)
    {
        var last = @this.Segments.LastOrDefault();

        if (last is null)
        {
            return 0;
        }

        if (last.EndsWith('/'))
        {
            last = last.Remove(last.Length - 1);
        }

        return Convert.ToInt16(last, CultureInfo.InvariantCulture);
    }
}
