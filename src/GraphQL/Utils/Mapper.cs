namespace GraphQL.Utils;

public class Mapper
{
    public static TTarget MapTo<TSource, TTarget>(TSource a)
        where TSource : notnull
        where TTarget : new()
    {
        // Create a new instance of the schema
        var b = new TTarget();

        // Get types
        var typeofA = a.GetType();
        var typeofB = b.GetType();

        // Copy fields
        foreach (var fieldofA in typeofA.GetFields())
        {
            var fieldofB = typeofB.GetField(fieldofA.Name);
            if (fieldofB is null)
            {
                continue;
            }
            fieldofB.SetValue(b, fieldofA.GetValue(a));
        }

        // copy properties
        foreach (var propertyofA in typeofA.GetProperties())
        {
            var propertyofB = typeofB.GetProperty(propertyofA.Name);
            if (propertyofB is null)
            {
                continue;
            }
            propertyofB.SetValue(b, propertyofA.GetValue(a));
        }

        return b;
    }
}
