namespace GraphQL.Exceptions;

using System.Globalization;

public class GraphQLException(string message) : Exception(message)
{
    public IEnumerable<string> Attributes { get; set; } = [];

    public override string ToString() =>
        string.Format(
            CultureInfo.CurrentCulture,
            "{0}: {1}",
            string.Join('.', this.Attributes),
            this.Message);
}
