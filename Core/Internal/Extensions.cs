using Moq;
using System.Linq.Expressions;
using System.Reflection;
namespace XspecT.Internal;

internal static class Extensions
{
    internal static Type GetMockedType(this Mock mock)
    {
        var mockType = mock.GetType();
        var mockedTypeProperty = mockType.GetProperty("MockedType", BindingFlags.NonPublic | BindingFlags.Instance);
        return mockedTypeProperty.GetValue(mock) as Type;
    }

    internal static string AsWords(this string str)
        => string.IsNullOrWhiteSpace(str)
        ? string.Empty
        : string.Join(' ', SplitWords(str).Select(word => word.ToLower()));

    internal static string Capitalize(this string str)
        => string.IsNullOrWhiteSpace(str)
        ? string.Empty
        : str[..1].ToUpper() + str[1..];

    internal static string GetName(this LambdaExpression expression)
    {
        const string defaultExpression = "<Expression>";
        if (expression is null)
            return defaultExpression;
        var body = expression.Body;
        var methodProperty = body.GetType().GetProperty("Method");
        var method = methodProperty?.GetValue(body);
        var nameProperty = method?.GetType().GetProperty("Name");
        var name = nameProperty?.GetValue(method) as string;
        return name?.Capitalize() ?? defaultExpression;
    }

    private static IEnumerable<string> SplitWords(string camelCase)
    {
        int fromIndex = 0;
        int toIndex = 0;
        for (var i = 0; i < camelCase.Length; i++)
        {
            if (!char.IsUpper(camelCase[i]))
                continue;
            toIndex = i;
            if (toIndex > fromIndex)
                yield return camelCase[fromIndex..toIndex];
            fromIndex = toIndex;
        }
        yield return camelCase[fromIndex..];
    }
}