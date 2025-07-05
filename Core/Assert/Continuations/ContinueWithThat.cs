using XspecT.Internal.Specification;

namespace XspecT.Assert.Continuations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TThat"></typeparam>
/// <param name="continuation"></param>
/// <param name="that"></param>
public class ContinueWithThat<TContinuation, TThat>(TContinuation continuation, TThat that)
    : ContinueWith<TContinuation>(continuation) where TContinuation : Constraint
{
    /// <summary>
    /// Continuation to apply assertions on the element
    /// </summary>
    public TThat That
    {
        get
        {
            SpecificationGenerator.AddThat();
            return that;
        }
    }
}