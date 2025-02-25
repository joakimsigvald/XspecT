using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record DoesValue<TValue> : Constraint<TValue, DoesValue<TValue>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="conditionExpr"></param>
    /// <returns></returns>
    public ContinueWithActual<TValue> Satisfy(
        Func<TValue, bool> condition,
        [CallerArgumentExpression(nameof(condition))] string conditionExpr = null)
    {
        Assert(conditionExpr.ParseValue(), () => Xunit.Assert.True(condition(Actual)), conditionExpr, "", "satisfies");
        return new(Actual);
    }
}