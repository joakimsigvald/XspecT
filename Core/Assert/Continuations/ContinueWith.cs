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

    internal TContinuation Or => Continue("or");

    private TContinuation Continue(string conjunction) 
    {
        SpecificationGenerator.AddAssertConjunction(conjunction);
        if (_continuation.IsEither && conjunction != "or")
            throw new SetupFailed("Cannot continue either with and or but, only with or");
        return _continuation with 
        { 
            ActualExpr = string.Empty, 
            AuxiliaryVerb = null, 
            Inverted = false,
            Exception = _continuation.Exception,
            IsEither = _continuation.IsEither && _continuation.Exception is null,
        };
    }
}