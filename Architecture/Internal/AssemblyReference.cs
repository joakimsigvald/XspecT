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
    public Assembly Assembly { get; }

    internal AssemblyReference(ArchSpec spec, Assembly assembly)
    {
        _spec = spec;
        Assembly = assembly;
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
        var other = _spec.Project(projectName);
        var referencedAssemblies = Assembly.GetReferencedAssemblies();
        var _ = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name)
            ?? throw new ArchitectureViolation($"{Assembly.GetName().Name} does not reference {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <exception cref="ArchitectureViolation"></exception>
    public void Use(string assemblyName)
    {
        var other = _spec.Assembly(assemblyName);
        var referencedAssemblies = Assembly.GetReferencedAssemblies();
        var _ = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name)
            ?? throw new ArchitectureViolation($"{Assembly.GetName().Name} does not reference {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void DoNotDependOn(string otherName)
    {
        var other = _spec.Project(otherName);
        var referencedAssemblies = Assembly.GetReferencedAssemblies();
        var referencedAssembly = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name);
        if (referencedAssembly is not null)
            throw new ArchitectureViolation($"{Assembly.GetName().Name} references {other.Assembly.GetName().Name}.");
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void DoNotUse(string otherName)
    {
        var other = _spec.Assembly(otherName);
        var referencedAssemblies = Assembly.GetReferencedAssemblies();
        var referencedAssembly = referencedAssemblies.SingleOrDefault(name => name.Name == other.Assembly.GetName().Name);
        if (referencedAssembly is not null)
            throw new ArchitectureViolation($"{Assembly.GetName().Name} references {other.Assembly.GetName().Name}.");
    }
}