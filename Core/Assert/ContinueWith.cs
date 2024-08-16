namespace XspecT.Assert;

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
    public TContinuation And
    {
        get
        {
            return _continuation with { ActualExpr = $"{Environment.NewLine}    and", AuxiliaryVerb = null };
        }
    }

    /// <summary>
    /// Get a continuation to make the next assertion
    /// </summary>
    public TContinuation But
    {
        get
        {
            return _continuation with { ActualExpr = $"{Environment.NewLine}    but", AuxiliaryVerb = null };
        }
    }
}