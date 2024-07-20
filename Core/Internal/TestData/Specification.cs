using System.Text;

namespace XspecT.Internal.TestData;

internal static class Specification 
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;

    internal static void Clear() => _specificationBuilder = null;

    internal static void AddSection(string section)
    {
        if (HasText)
            Builder.Append(",");
        AddSubSection(section);
    }

    internal static void AddSubSection(string phrase)
    {
        if (HasText)
            Builder.Append(" ");
        Builder.Append(phrase);
    }

    internal static string Description => Builder.ToString().TrimStart().Capitalize();

    private static bool HasText => _specificationBuilder is not null;
    private static StringBuilder Builder => _specificationBuilder ??= new();
}