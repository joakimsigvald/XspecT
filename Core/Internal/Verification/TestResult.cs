using Moq;
using System.Linq.Expressions;
using XspecT.Fixture;
using XspecT.Internal.TestData;
using XspecT.Verification;

namespace XspecT.Internal.Verification;

internal class TestResult<TResult> : ITestResult<TResult>
{
    private readonly Exception _error;
    private readonly Context _context;
    private readonly bool _hasResult;
    private TResult _result;

    internal TestResult(TResult result, Exception error, Context context, bool hasResult)
    {
        Result = result;
        _error = error;
        _context = context;
        _hasResult = hasResult;
    }

    public TResult Result
    {
        get => _hasResult && _error is null
            ? _result
            : throw _error
            ?? new SetupFailed(
@"Tried to use Result, but an action, or func with different return type, was provided as method under test (When). 
Try providing a function with the Spec's declared return type instead as parameter to When");
        init => _result = value;
    }

    public IAndThen<TResult> Throws<TError>()
    {
        Assert.IsType<TError>(_error);
        return And();
    }

    public IAndThen<TResult> Throws<TError>(Func<TError> error) where TError : Exception
    {
        Assert.IsType<TError>(_error).Is(error());
        return And();
    }

    public IAndThen<TResult> Throws<TError>(Action<TError> assert)
    {
        var ex = Assert.IsType<TError>(_error);
        assert(ex);
        return And();
    }

    public IAndThen<TResult> Throws()
    {
        _error.Is().NotNull();
        return And();
    }

    public IAndThen<TResult> DoesNotThrow<TError>()
    {
        Assert.IsNotType<TError>(_error);
        return And();
    }

    public IAndThen<TResult> DoesNotThrow()
    {
        _error.Is().Null();
        return And();
    }

    internal IAndVerify<TResult> Verify<TService>(Expression<Action<TService>> expression) where TService : class
        => CombineWithErrorOnFail(() => Mocked<TService>().Verify(expression));

    internal IAndVerify<TResult> Verify<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    internal IAndVerify<TResult> Verify<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    internal IAndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression));

    internal IAndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    internal IAndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
        => CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));

    private Mock<TObject> Mocked<TObject>() where TObject : class => _context.GetMock<TObject>();

    private IAndVerify<TResult> CombineWithErrorOnFail(Action verify)
    {
        try
        {
            verify();
            return new AndVerify<TResult>(this);
        }
        catch (Exception ex)
        {
            if (_error is null)
                throw;
            throw new AggregateException(ex, _error);
        }
    }

    private IAndThen<TResult> And() => new AndThen<TResult>(this);
}