﻿using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;

namespace XspecT.Verification;

public class TestResult<TResult>
{
    private readonly Exception _error;
    private readonly AutoMocker _mocker;
    private readonly bool _hasResult;
    private TResult _result;

    public TestResult(TResult result, Exception error, AutoMocker mocker, bool hasResult)
    {
        Result = result;
        _error = error;
        _mocker = mocker;
        _hasResult = hasResult;
    }

    public TResult Result
    {
        get => _hasResult && _error is null 
            ? _result 
            : throw _error 
            ?? new SetupFailed("Tried to use Result, but an action, or func with different return type, was provided as method under test (When). Try providing a function with the Spec's declared return type instead as parameter to When");
        init => _result = value;
    }

    public AndThen<TResult> Throws<TError>()
    {
        Assert.IsType<TError>(_error);
        return new(this);
    }

    public AndThen<TResult> Throws<TError>(TError error) where TError : Exception
    {
        error.Is(Assert.IsType<TError>(_error));
        return new(this);
    }

    public AndThen<TResult> Throws<TError>(Action<TError> assert)
    {
        var ex = Assert.IsType<TError>(_error);
        assert(ex);
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

    internal AndVerify<TResult> Verify<TService>(Expression<Action<TService>> expression) where TService : class
    {
        CombineWithErrorOnFail(() => Mocked<TService>().Verify(expression));
        return new AndVerify<TResult>(this);
    }

    internal AndVerify<TResult> Verify<TObject>(Expression<Action<TObject>> expression, Times times) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndVerify<TResult>(this);
    }

    internal AndVerify<TResult> Verify<TObject>(Expression<Action<TObject>> expression, Func<Times> times) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndVerify<TResult>(this);
    }

    internal AndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression) where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression));
        return new AndVerify<TResult>(this);
    }

    internal AndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Times times)
        where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndVerify<TResult>(this);
    }

    internal AndVerify<TResult> Verify<TObject, TReturns>(Expression<Func<TObject, TReturns>> expression, Func<Times> times)
        where TObject : class
    {
        CombineWithErrorOnFail(() => Mocked<TObject>().Verify(expression, times));
        return new AndVerify<TResult>(this);
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