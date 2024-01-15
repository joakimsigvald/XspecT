using NetArchTest.Rules;
using XspecT.Architecture.Assertion;

namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class PredicateListContinuation
{
    internal readonly PredicateList _predicateList;

    internal PredicateListContinuation(PredicateList predicateList) 
        => _predicateList = predicateList;

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public Predicates That() => _predicateList.And();

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public DoesTypes Does() => new(_predicateList.Should());
}