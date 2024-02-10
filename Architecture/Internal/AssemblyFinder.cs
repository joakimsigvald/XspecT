using System.Reflection;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Internal;

internal class AssemblyFinder
{
    private readonly string _solutionNamespace;
    private readonly string _assemblyName;
    private readonly bool _isProject;
    private readonly List<string> _searchedChildren = new();

    internal AssemblyFinder(string solutionNamespace, string assemblyName, bool isProject)
    {
        _solutionNamespace = solutionNamespace;
        _assemblyName = assemblyName;
        _isProject = isProject;
    }

    internal (Assembly assembly, string wildcardMatch)[] FindAssemblies(Assembly root)
    {
        var res = Find(root).ToList();
        var (found, wildcardMatch) = GetMatch(root.GetName());
        if (found)
            res.Add((root, wildcardMatch));
        return res.Any() ? res.ToArray() : throw new InvalidExpectation(GetErrorMessage());
    }

    private IEnumerable<(Assembly, string)> Find(Assembly root)
    {
        var directReferences = root.GetReferencedAssemblies();
        var found = directReferences
            .Select(a => (a, match: GetMatch(a)))
            .Where(t => t.match.found)
            .Select(t => (Assembly.Load(t.a), t.match.wildcardMatch));
        return found.Concat(FindDescendant(directReferences));
    }

    private IEnumerable<(Assembly, string)> FindDescendant(AssemblyName[] directReferences)
        => AddNewReferences(directReferences)
        .Where(nr => nr.Name.StartsWith(_solutionNamespace))
        .Select(Assembly.Load)
        .SelectMany(Find);

    private AssemblyName[] AddNewReferences(AssemblyName[] directReferences)
    {
        var newReferences = directReferences
                .Where(c => !_searchedChildren.Contains(c.Name))
                .ToArray();
        _searchedChildren.AddRange(newReferences.Select(nc => nc.Name));
        return newReferences;
    }

    private (bool found, string wildcardMatch) GetMatch(AssemblyName assembyName)
    {
        var name = assembyName.Name;
        if (_assemblyName.Contains('*'))
        {
            if (!name.StartsWith(_solutionNamespace))
                return (false, null);
            var wildcardIndex = _assemblyName.IndexOf('*');
            var shortName = name[(_solutionNamespace.Length + 1)..];
            var prefix = _assemblyName[..wildcardIndex];
            var postfix = _assemblyName[(wildcardIndex + 1)..];
            if (!shortName.StartsWith(prefix))
                return (false, null);
            if (!shortName.EndsWith(postfix))
                return (false, null);
            var wildcardMatch = shortName[prefix.Length..^postfix.Length];
            return (true, wildcardMatch);
        }
        var found = name == (_isProject ? $"{_solutionNamespace}.{_assemblyName}" : _assemblyName);
        return (found, null);
    }

    private string GetErrorMessage()
    {
        var similarAssemblyName = _searchedChildren.FirstOrDefault(assemblyName => assemblyName.Contains(_assemblyName));
        return similarAssemblyName is null
            ? $"No assembly by the name {_assemblyName} was found. Did you forget to reference the assembly in the test project?"
            : $"No assembly by the name {_assemblyName} was found. Did you mean {similarAssemblyName}?";
    }
}