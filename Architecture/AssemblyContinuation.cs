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
        var rootAssembly = Assembly.GetCallingAssembly();
        var dependencies = rootAssembly.GetReferencedAssemblies();
        return FindAssembly() ?? throw new InvalidExpectation(GetErrorMessage());

        Assembly FindAssembly()
            => dependencies.Select(Assembly.Load).Prepend(rootAssembly)
            .FirstOrDefault(_ => _.GetName().Name == name);

        string GetErrorMessage()
        {
            var names = dependencies.Prepend(rootAssembly.GetName())
                .Select(_ => _.Name).OrderBy(_ => _).ToArray();
            var similarAssemblyName = names.FirstOrDefault(assemblyName => assemblyName.Contains(name));
            return similarAssemblyName is null
                ? $"No assembly by the name {name} was found. Did you forget to reference the assembly in the test project?"
                : $"No assembly by the name {name} was found. Did you mean {similarAssemblyName}?";
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Assembly Of<T>() => typeof(T).Assembly;
}