using System.Reflection;
using XspecT.Architecture.Exceptions;

namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public interface IAssemblyReference
{
    /// <summary>
    /// TODO
    /// </summary>
    Assembly Assembly { get; }

    /// <summary>
    /// TODO
    /// </summary>
    PredicateListContinuation Classes { get; }

    /// <summary>
    /// TODO
    /// </summary>
    PredicateListContinuation Interfaces { get; }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="projectName"></param>
    /// <exception cref="ArchitectureViolation"></exception>
    void DependOn(string projectName);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <exception cref="ArchitectureViolation"></exception>
    void Use(string assemblyName);

    /// <summary>
    /// TODO
    /// </summary>
    void DoNotDependOn(string otherName);

    /// <summary>
    /// TODO
    /// </summary>
    void DoNotUse(string otherName);
}