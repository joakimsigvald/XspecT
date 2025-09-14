using System.Collections;

namespace XspecT.Internal.Specification;

internal static class ObjectExtensions
{
    internal static string ParseValue(this object? value)
        => value switch
        {
            null => "null",
            string s => $"\"{value}\"",
            IEnumerable col => $"[{string.Join(", ", col.Cast<object?>().Select(ParseValue))}]",
            bool b => b ? "true" : "false",
            _ => value.ToString()
        } ?? "null";
}