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

    internal static string GetMethodName(this LambdaExpression expression)
    {
        var methodProperty = expression.Body.GetType().GetProperty("Method");
        var method = methodProperty?.GetValue(expression.Body);
        var nameProperty = method?.GetType().GetProperty("Name");
        var name = nameProperty?.GetValue(method) as string;
        return name?.Capitalize() ?? "<Expression>";
    }

    private static IEnumerable<string> SplitWords(string camelCase)
    {
        int fromIndex = 0;
        for (var toIndex = 0; toIndex < camelCase.Length; toIndex++)
        {
            if (!char.IsUpper(camelCase[toIndex]))
                continue;
            if (toIndex > fromIndex)
                yield return camelCase[fromIndex..toIndex];
            fromIndex = toIndex;
        }
        yield return camelCase[fromIndex..];
    }
}