﻿namespace XspecT.Internal.Specification;

internal static class StringExtensions
{
    internal static string AsWords(this string str)
        => string.Join(' ', str.ToWords());

    internal static string[] ToWords(this string str)
        => string.IsNullOrWhiteSpace(str)
        ? [string.Empty]
        : [.. SplitWords(str).Select(word => word.ToLower())];

    internal static string Capitalize(this string str)
        => string.IsNullOrWhiteSpace(str)
        ? string.Empty
        : str[..1].ToUpper() + str[1..];

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