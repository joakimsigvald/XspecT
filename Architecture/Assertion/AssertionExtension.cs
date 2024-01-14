using NetArchTest.Rules;
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

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static AreTypes Are(this Types types) => new(types.Should());

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public static AreTypes Are(this PredicateList predicates) => new(predicates.Should());
}