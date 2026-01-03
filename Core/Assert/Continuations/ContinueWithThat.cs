using System.Diagnostics.CodeAnalysis;
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
    [Obsolete("Use that instead")]
    public TThat That
    {
        get
        {
            SpecificationGenerator.AddThat();
            return that;
        }
    }

    /// <summary>
    /// Continuation to apply assertions on the element
    /// </summary>
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Special convension of binding words")]
    public TThat that
    {
        get
        {
            SpecificationGenerator.AddThat();
            return that;
        }
    }
}