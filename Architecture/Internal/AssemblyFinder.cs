using System.Reflection;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Internal;

internal class AssemblyFinder
{
    private readonly string _solutionNamespace;
    private readonly string _assemblyName;
    private readonly List<string> _searchedChildren = new();

    internal AssemblyFinder(string solutionNamespace, string assemblyName)
    {
        _solutionNamespace = solutionNamespace;
        _assemblyName = assemblyName;
    }

    internal Assembly FindAssembly(Assembly root) 
        => IsMatch(root.GetName()) ? root
        : Find(root) ?? throw new InvalidExpectation(GetErrorMessage());

    private Assembly Find(Assembly root)
    {
        var directReferences = root.GetReferencedAssemblies();
        var found = directReferences.FirstOrDefault(IsMatch);
        return found is null ? FindDescendant(directReferences) : Assembly.Load(found);
    }

    private Assembly FindDescendant(AssemblyName[] directReferences) 
        => AddNewReferences(directReferences).Where(nr => nr.Name.StartsWith(_solutionNamespace))
            .Select(Assembly.Load)
            .Select(Find)
            .FirstOrDefault(_ => _ != null);

    private AssemblyName[] AddNewReferences(AssemblyName[] directReferences)
    {
        var newReferences = directReferences
                .Where(c => !_searchedChildren.Contains(c.Name))
                .ToArray();
        _searchedChildren.AddRange(newReferences.Select(nc => nc.Name));
        return newReferences;
    }

    private bool IsMatch(AssemblyName assemblyName)
        => new[] { _assemblyName, $"{_solutionNamespace}.{_assemblyName}" }.Contains(assemblyName.Name);

    private string GetErrorMessage()
    {
        var similarAssemblyName = _searchedChildren.FirstOrDefault(assemblyName => assemblyName.Contains(_assemblyName));
        return similarAssemblyName is null
            ? $"No assembly by the name {_assemblyName} was found. Did you forget to reference the assembly in the test project?"
            : $"No assembly by the name {_assemblyName} was found. Did you mean {similarAssemblyName}?";
    }
}