using XspecT.Internal.Specification;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Return-value from an assertion, that allows another assertion to be chained to the previous (if it succeeded)
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
public class ContinueWith<TContinuation> where TContinuation : Constraint
{
    private readonly TContinuation _continuation;
    internal ContinueWith(TContinuation continuation) => _continuation = continuation;

    /// <summary>
    /// Get a continuation to make the next assertion
    /// </summary>
    public TContinuation And => Continue("and");

    /// <summary>
    /// Get a continuation to make the next assertion
    /// </summary>
    public TContinuation But => Continue("but");

    private TContinuation Continue(string conjunction) 
    {
        SpecificationGenerator.AddAssertConjunction(conjunction);
        return _continuation with { ActualExpr = string.Empty, AuxiliaryVerb = null, Inverted = false };
    }
}