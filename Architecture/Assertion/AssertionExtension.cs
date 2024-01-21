using NetArchTest.Rules;

namespace XspecT.Architecture.Assertion;

/// <summary>
/// TODO
/// </summary>
public static class AssertionExtension
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="predicates"></param>
    /// <returns></returns>
    public static AreTypes Are(this PredicateList predicates) => new(predicates.Should());
}