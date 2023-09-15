using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public interface IAndVerify<TResult> : IAndThen<TResult>
{
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression) where TObject : class;
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class;
    public IAndVerify<TResult> And<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class;
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class;
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class;
    public IAndVerify<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class;
}