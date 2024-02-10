using XspecT.Architecture.Exceptions;
using XspecT.Architecture.Internal;

namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public abstract class ArchSpec
{
    private readonly string _solutionNamespace;
    private readonly System.Reflection.Assembly _testAssembly;

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="solutionNamespace"></param>
    protected ArchSpec(string solutionNamespace)
    {
        _solutionNamespace = solutionNamespace;
        _testAssembly = System.Reflection.Assembly.GetCallingAssembly();
    }

    /// <summary>
    /// Find an assembly, referenced directly or indirectly from test project, by name
    /// </summary>
    /// <param name="name">the namespace of the assembly</param>
    /// <returns>The assembly if found</returns>
    /// <exception cref="InvalidExpectation"></exception>
    public IAssemblyReference Project(string name)
        => new AssemblyReference(
            this, new AssemblyFinder(_solutionNamespace, name, true).FindAssembly(_testAssembly));

    /// <summary>
    /// Find an assembly, referenced directly or indirectly from test project, by name
    /// </summary>
    /// <param name="name">the namespace of the assembly</param>
    /// <returns>The assembly if found</returns>
    /// <exception cref="InvalidExpectation"></exception>
    public IAssemblyReference Assembly(string name)
        => new AssemblyReference(
            this, new AssemblyFinder(_solutionNamespace, name, false).FindAssembly(_testAssembly));
}