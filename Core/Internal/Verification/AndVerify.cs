using Moq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;
using XspecT.Internal.Specification;

namespace XspecT.Internal.Verification;

internal class AndVerify<TSUT, TResult> : AndThen<TSUT, TResult>, IAndVerify<TResult>
{
    internal AndVerify(TestResult<TSUT, TResult> parent) : base(parent) { }

    public IAndVerify<TResult> And<TObject>(
        Expression<Action<TObject>> expression,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null) 
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, expressionExpr!);
    }

    public IAndVerify<TResult> And<TObject>(
        Expression<Action<TObject>> expression, Times times,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null) 
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, times, expressionExpr!);
    }

    public IAndVerify<TResult> And<TObject>(
        Expression<Action<TObject>> expression, Func<Times> times,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null) 
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, times, expressionExpr!);
    }

    public IAndVerify<TResult> And<TObject, TReturns>(
        Expression<Func<TObject, TReturns>> expression,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null) 
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, expressionExpr!);
    }

    public IAndVerify<TResult> And<TObject, TReturns>(
        Expression<Func<TObject, TReturns>> expression, Times times,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null)
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, times, expressionExpr!);
    }

    public IAndVerify<TResult> And<TObject, TReturns>(
        Expression<Func<TObject, TReturns>> expression, Func<Times> times,
        [CallerArgumentExpression(nameof(expression))] string? expressionExpr = null)
        where TObject : class
    {
        SpecificationGenerator.AddThen();
        return Parent.Verify(expression, times, expressionExpr!);
    }
}