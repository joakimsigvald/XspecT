using Moq;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using Xunit.Sdk;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        get => _hasResult && _error is null ? _result : throw UnexpectedError;
        init => _result = value;
    }

    public IAndThen<TResult> Throws<TError>()
    {
        Specification.AddAssertThrows<TError>();
        AssertError<TError>();
        return And();
    }

    public IAndThen<TResult> Throws<TError>(Func<TError> error) where TError : Exception
    {
        AssertError(error());
        return And();
    }

    public IAndThen<TResult> Throws<TError>(Action<TError> assert)
    {
        AssertError(assert);
        return And();
    }

    public IAndThen<TResult> Throws()
    {
        AssertError<Exception>();
        return And();
    }

    public IAndThen<TResult> DoesNotThrow<TError>()
    {
        AssertNoError<TError>();
        return And();
    }

    public IAndThen<TResult> DoesNotThrow()
    {
        Specification.AddAssert();
        AssertNoError<Exception>();
        return And();
    }

    internal IAndVerify<TResult> Verify<TService>(Expression<Action<TService>> expression, string expressionExpr = null) 
        where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression), expressionExpr);

    internal IAndVerify<TResult> Verify<TService>(Expression<Action<TService>> expression, Times times) where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression, times));

    internal IAndVerify<TResult> Verify<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression, times));

    internal IAndVerify<TResult> Verify<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression));

    internal IAndVerify<TResult> Verify<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression, times));

    internal IAndVerify<TResult> Verify<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class
        => CombineWithErrorOnFail<TService>(mock => mock.Verify(expression, times));

    private void AssertError<TError>(TError expected)
        where TError : Exception
    {
        var actual = AssertError<TError>();
        if (expected != actual)
            throw new XunitException(
                $"Expected the exception {expected}, but {actual} was thrown");
    }

    private void AssertError<TError>(Action<TError> assert)
    {
        try
        {
            assert(AssertError<TError>());
        }
        catch (Exception ex)
        {
            throw new XunitException($"Thrown exception {typeof(TError)} didn't meet expectations", ex);
        }
    }

    private TError AssertError<TError>()
        => _error is TError err
        ? err
        : throw new XunitException(
            $"Expected {typeof(TError)}, but {_error?.GetType().Name ?? "No exception"} was thrown");

    private void AssertNoError<TError>()
    {
        if (_error is TError)
            throw new XunitException($"Expected not to throw {typeof(TError)}, but threw {_error.GetType().Name}");
    }

    private Exception UnexpectedError
        => _error ?? new SetupFailed(
@"Tried to use Result, but an action, or func with different return type, was provided as method under test (When). 
Try providing a function with the Spec's declared return type instead as parameter to When");

    private Mock<TObject> Mocked<TObject>() where TObject : class => _context.GetMock<TObject>();

    private AndVerify<TResult> CombineWithErrorOnFail<TService>(Action<Mock<TService>> verify, string expressionExpr = null)
        where TService : class
    {
        try
        {
            Specification.AddVerify<TService>(expressionExpr);
            verify(Mocked<TService>());
            return new AndVerify<TResult>(this);
        }
        catch (Exception ex)
        {
            if (_error is null)
                throw;
            throw new AggregateException(ex, _error);
        }
    }

    private AndThen<TResult> And() => new(this);
}