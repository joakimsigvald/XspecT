using System.Reflection;
using XspecT.Architecture.Exceptions;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class AssemblyContinuation
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Assembly Named(string name)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var assembly = assemblies.SingleOrDefault(assembly => assembly.GetName().Name == name);
        if (assembly != null)
            return assembly;
        var names = assemblies.Select(ass => ass.GetName().Name).OrderBy(_ => _).ToArray();
        var similarAssemblyName = names.FirstOrDefault(assemblyName => assemblyName.Contains(name))
            ?? throw new InvalidExpectation($"No assembly by the name {name} was found. Did you forget to reference the assembly in the test project?");
        throw new InvalidExpectation($"No assembly by the name {name} was found. Did you mean {similarAssemblyName}?");
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Assembly Of<T>() => typeof(T).Assembly;
}