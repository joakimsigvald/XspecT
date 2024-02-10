using NetArchTest.Rules;
using System.Reflection;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Internal;

/// <summary>
/// TODO
/// </summary>
internal class AssemblyReference : IAssemblyReference
{
    private readonly ArchSpec _spec;
    private (Assembly assembly, string wildcardMatch)[] _matches { get; }
    public Assembly Assembly => _matches.FirstOrDefault().assembly;
    private string _wildcardMatch;

    internal AssemblyReference(ArchSpec spec, (Assembly, string)[] matches)
    {
        _spec = spec;
        _matches = matches;
    }

    /// <summary>
    /// TODO
    /// </summary>
    public PredicateListContinuation Classes
    => new(Types.InAssembly(Assembly).That().AreClasses());

    /// <summary>
    /// TODO
    /// </summary>
    public PredicateListContinuation Interfaces
    => new(Types.InAssembly(Assembly).That().AreInterfaces());

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="projectName"></param>
    /// <exception cref="ArchitectureViolation"></exception>
    public void DependOn(string projectName)
    {
        foreach (var match in _matches)
            DependsOn(match, projectName);
    }

    private void DependsOn((Assembly assembly, string wildcardMatch) match, string projectName)
    {
        var specificName = projectName.Replace("*", match.wildcardMatch);
        var other = _spec.Project(specificName);
        var referencedAssemblies = match.assembly.GetReferencedAssemblies();
        var _ = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name)
            ?? throw new ArchitectureViolation($"{match.assembly.GetName().Name} does not reference {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <exception cref="ArchitectureViolation"></exception>
    public void Use(string assemblyName)
    {
        foreach (var assembly in _matches)
            Uses(assembly, assemblyName);
    }

    private void Uses((Assembly assembly, string wildcardMatch) match, string assemblyName)
    {
        var other = _spec.Assembly(assemblyName);
        var referencedAssemblies = match.assembly.GetReferencedAssemblies();
        var _ = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name)
            ?? throw new ArchitectureViolation($"{match.assembly.GetName().Name} does not reference {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void DoNotDependOn(string projectName)
    {
        foreach (var assembly in _matches)
            DoesNotDependOn(assembly, projectName);
    }

    private void DoesNotDependOn((Assembly assembly, string wildcardMatch) match, string otherName)
    {
        var other = _spec.Project(otherName);
        var referencedAssemblies = match.assembly.GetReferencedAssemblies();
        var referencedAssembly = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name);
        if (referencedAssembly is not null)
            throw new ArchitectureViolation($"{match.assembly.GetName().Name} references {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void DoNotUse(string assemblyName)
    {
        foreach (var assembly in _matches)
            DoesNotUse(assembly, assemblyName);
    }

    private void DoesNotUse((Assembly assembly, string wildcardMatch) match, string otherName)
    {
        var other = _spec.Assembly(otherName);
        var referencedAssemblies = match.assembly.GetReferencedAssemblies();
        var referencedAssembly = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name);
        if (referencedAssembly is not null)
            throw new ArchitectureViolation($"{match.assembly.GetName().Name} references {other.Assembly.GetName().Name}.");
    }
}