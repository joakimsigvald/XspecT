using System.Reflection;

namespace XspecT.Architecture.Assertion;

/// <summary>
/// TODO
/// </summary>
public static class AssertionExtension
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static DoesAssembly Does(this Assembly assembly) => new(assembly);
}