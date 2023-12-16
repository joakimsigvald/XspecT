using Moq;
using System.Linq.Expressions;

namespace XspecT.Internal.Verification;

internal class AndVerify<TResult> : AndThen<TResult>, IAndVerify<TResult>
{
    internal AndVerify(TestResult<TResult> parent) : base(parent) { }

    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression) where TObject : class
        => Parent.Verify(expression);

    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
        => Parent.Verify(expression, times);

    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
        => Parent.Verify(expression, times);

    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
        => Parent.Verify(expression);

    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
        => Parent.Verify(expression, times);

    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
        => Parent.Verify(expression, times);
}