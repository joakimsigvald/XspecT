using System.Text;

namespace XspecT.Internal.TestData;

internal static class Specification 
{
    [ThreadStatic]
    private static StringBuilder _specificationBuilder;

    [ThreadStatic]
    private static Stack<string> _fragments;

    internal static void Clear()
    {
        _fragments = null;
        _specificationBuilder = null;
    }

    internal static void AddSection(string section)
    {
        if (_specificationBuilder is not null)
            AddFragment(",");
        AddSubSection(section);
    }

    internal static void AddSubSection(string phrase)
    {
        if (_specificationBuilder is not null)
            AddFragment(" ");
        AddFragment(phrase);
        while (_fragments?.Count > 0)
        {
            var fragment = _fragments.Pop();
            if (fragment is null)
                return;
            AddFragment(fragment);
        }
    }

    internal static void AddFragment(string fragment)
    {
        _specificationBuilder ??= new();
        _specificationBuilder.Append(fragment);
    }

    internal static void PushFragment(string fragment)
    {
        _fragments ??= new();
        _fragments.Push(fragment);
    }

    internal static void PushStop() => PushFragment(null);

    /// <summary>
    /// 
    /// </summary>
    internal static string Description
        => _specificationBuilder?.ToString().TrimStart().Capitalize()
        ?? string.Empty;
}
