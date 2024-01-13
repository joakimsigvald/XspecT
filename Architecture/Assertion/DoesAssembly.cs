using System.Reflection;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture.Assertion;

/// <summary>
/// TODO
/// </summary>
public class DoesAssembly : Constraint<DoesAssembly, Assembly>
{
    internal DoesAssembly(Assembly actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesAssembly> DependOn(Assembly other)
    {
        var referencedAssemblies = _actual.GetReferencedAssemblies();
        var _ = referencedAssemblies.SingleOrDefault(name => name.Name == other.GetName().Name) 
            ?? throw new ArchitectureViolation($"{_actual.GetName().Name} should reference {other.GetName().Name}, but does.");
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesAssembly> NotDependOn(Assembly other)
    {
        var referencedAssemblies = _actual.GetReferencedAssemblies();
        var referencedAssembly = referencedAssemblies.SingleOrDefault(name => name.Name == other.GetName().Name);
        if (referencedAssembly is not null)
            throw new ArchitectureViolation($"{_actual.GetName().Name} should not reference {other.GetName().Name}, but doesn't");
        return And();
    }
}