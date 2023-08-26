using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;

namespace XspecT.Verification;

public class TestResult<TResult>
{
    private readonly Exception _error;
    private readonly AutoMocker _mocker;
    private TResult _result;

    public TestResult(TResult result, Exception error, AutoMocker mocker)
    {
        Result = result;
        _error = error;
        _mocker = mocker;
    }

    public TResult Result { get => _error is null ? _result : throw _error; init => _result = value; }
    public AndThen<TResult> Throws<TError>()
    {
        Assert.IsType<TError>(_error);
        return new(this);
    }

    public AndThen<TResult> Throws()
    {
        _error.Is().NotNull();
        return new(this);
    }

    public AndThen<TResult> DoesNotThrow<TError>()
    {
        Assert.IsNotType<TError>(_error);
        return new(this);
    }

    public AndThen<TResult> DoesNotThrow()
    {
        _error.Is().Null();
        return new(this);
    }

    public AndThen<TResult> Throws<TError>(Action<TError> assert)
    {
        var ex = Assert.IsType<TError>(_error);
        assert(ex);
        return new(this);
    }

    internal AndDoes<TResult> Does<TService>(Expression<Action<TService>> expression) where TService : class
    {
        CombineWithErrorOnFail(() => Mocked<TService>().Verify(expression));
        return new AndDoes<TResult>(this);
    }

    internal AndDoes<TResult> Does<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndDoes<TResult>(this);
    }

    internal AndDoes<TResult> Does<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndDoes<TResult>(this);
    }

    internal AndDoes<TResult> Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression));
        return new AndDoes<TResult>(this);
    }

    internal AndDoes<TResult> Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndDoes<TResult>(this);
    }

    internal AndDoes<TResult> Does<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndDoes<TResult>(this);
    }

    private Mock<TObject> Mocked<TObject>() where TObject : class => _mocker.GetMock<TObject>();

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