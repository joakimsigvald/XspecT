using Moq;
using System.Linq.Expressions;

namespace XspecT.Verification;

public class TestResult<TResult>
{
    private readonly IMocking _mocking;
    private readonly Exception _error;
    private TResult _result;

    public TestResult(TResult result, Exception error, IMocking mocking)
    {
        Result = result;
        _error = error;
        _mocking = mocking;
    }

    public TResult Result { get => _error is null ? _result : throw _error; init => _result = value; }
    public void Throws<TError>() => Assert.IsType<TError>(_error);
    public void Throws() => Assert.NotNull(_error);
    public void NotThrows<TError>() => Assert.IsNotType<TError>(_error);
    public void NotThrows() => Assert.Null(_error);

    public void Throws<TError>(Action<TError> assert)
    {
        var ex = Assert.IsType<TError>(_error);
        assert(ex);
    }

    public void Does<TObject>(Expression<Action<TObject>> expression) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression));

    public void Does<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    public void Does<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    public void Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression));

    public void Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    public void Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    private Mock<TObject> Mocked<TObject>() where TObject : class => _mocking._<TObject>();

    private void CombineWithErrorOnFail(Action verify)
    {
        try
        {
            verify();
        }
        catch (Exception ex)
        {
            if (_error is null)
                throw;
            throw new AggregateException(ex, _error);
        }
    }
}