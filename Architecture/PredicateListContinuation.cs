using NetArchTest.Rules;
namespace XspecT.Architecture;

/// <summary>
/// TODO
/// </summary>
public class PredicateListContinuation
{
    private readonly PredicateList _predicateList;

    internal PredicateListContinuation(PredicateList predicateList) 
        => _predicateList = predicateList;

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public Predicates That() => _predicateList.And();
}