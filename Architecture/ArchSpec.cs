using System.Reflection;
using XspecT.Architecture.Exceptions;
using XspecT.Architecture.Internal;

namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class ArchSpec
{
    /// <summary>
    /// TODO
    /// </summary>
    protected static IClassesContinuation Classes => new ClassesContinuation();

    /// <summary>
    /// TODO
    /// </summary>
    protected static InterfacesContinuation Interfaces => new();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Assembly AssemblyOf<T>() => typeof(T).Assembly;

    /// <summary>
    /// Find an assembly, referenced directly or indirectly from test project, by name
    /// </summary>
    /// <param name="name">the namespace of the assembly</param>
    /// <returns>The assembly if found</returns>
    /// <exception cref="InvalidExpectation"></exception>
    public static Assembly AssemblyNamed(string name)
    {
        var root = Assembly.GetCallingAssembly();
        if (root.GetName().Name == name)
            return root;

        var searchedChildren = new List<string>();
        return Find(root, name, searchedChildren)
            ?? throw new InvalidExpectation(GetErrorMessage(searchedChildren, name));
    }

    private static Assembly Find(Assembly root, string name, List<string> searchedChildren)
    {
        var directReferences = root.GetReferencedAssemblies();
        var found = directReferences.FirstOrDefault(dr => dr.Name == name);
        if (found != null)
            return Assembly.Load(found);

        var newReferences = directReferences
            .Where(c => !searchedChildren.Contains(c.Name))
            .ToArray();
        searchedChildren.AddRange(newReferences.Select(nc => nc.Name));
        return newReferences.Where(HasSameRootNamespace)
            .Select(_ => Find(Assembly.Load(_), name, searchedChildren))
            .FirstOrDefault(_ => _ != null);

        bool HasSameRootNamespace(AssemblyName a1)
            => GetRootNamespace(a1.Name) == GetRootNamespace(root.FullName);

        string GetRootNamespace(string name) => name?.Split('.')[0];
    }

    private static string GetErrorMessage(List<string> searchedChildren, string name)
    {
        var names = searchedChildren.OrderBy(_ => _).ToArray();
        var similarAssemblyName = names.FirstOrDefault(assemblyName => assemblyName.Contains(name));
        return similarAssemblyName is null
            ? $"No assembly by the name {name} was found. Did you forget to reference the assembly in the test project?"
            : $"No assembly by the name {name} was found. Did you mean {similarAssemblyName}?";
    }
}