using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public class AndVerify<TResult> : AndThen<TResult>
{
    public AndVerify(TestResult<TResult> parent) : base(parent) { }

    public AndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression) where TObject : class
        => Parent.Verify(expression);

    public AndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
        => Parent.Verify(expression, times);

    public AndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
        => Parent.Verify(expression, times);

    public AndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
        => Parent.Verify(expression);

    public AndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
        => Parent.Verify(expression, times);

    public AndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
        => Parent.Verify(expression, times);
}