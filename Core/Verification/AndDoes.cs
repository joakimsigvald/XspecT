using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public class AndDoes<TResult>
{
    private readonly TestResult<TResult> _parent;

    public AndDoes(TestResult<TResult> parent) => _parent = parent;

    public AndDoes<TResult> And<TObject>(Expression<Action<TObject>> expression) where TObject : class
        => _parent.Does(expression);

    public AndDoes<TResult> And<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
        => _parent.Does(expression, times);

    public AndDoes<TResult> And<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
        => _parent.Does(expression, times);

    public AndDoes<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
        => _parent.Does(expression);

    public AndDoes<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
        => _parent.Does(expression, times);

    public AndDoes<TResult> And<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
        => _parent.Does(expression, times);
}