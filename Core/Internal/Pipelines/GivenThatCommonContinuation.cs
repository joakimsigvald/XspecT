using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TMock>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
    where TMock : IFluentInterface
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Lazy<TMock> _lazyContinuation;
    protected readonly string _callExpr;
    protected readonly string _tapExpr;

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, Lazy<TMock> continuation, string callExpr, string tapExpr = null)
    {
        _spec = spec;
        _lazyContinuation = continuation;
        _callExpr = callExpr;
        _tapExpr = tapExpr;
    }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec,
        Expression<Action<TService>> call,
        string callExpr = null)
        : this(spec, new Lazy<TMock>(() => (TMock)spec.GetMock<TService>().Setup(call)), callExpr)
    {
    }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec,
        Func<Mock<TService>, TMock> setup,
        string callExpr = null)
        : this(spec, new Lazy<TMock>(() => setup(spec.GetMock<TService>())), callExpr)
    {
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns()
    {
        SetupReturns();
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns(
        [NotNull] Func<TReturns> returns, [CallerArgumentExpression(nameof(returns))] string returnsExpr = null)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        SetupReturns(returns, returnsExpr);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> ReturnsDefault()
        => Returns(() => default);

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws<TException>()
        where TException : Exception, new()
    {
        SetupThrows<TException>();
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(
        Func<Exception> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SetupThrows(expected, expectedExpr);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    protected TMock Continuation => _lazyContinuation.Value;

    private void SetupReturns()
    {
        _spec.ArrangeLast(DoSetupReturns);

        void DoSetupReturns()
        {
            SpecifyMock();
            SpecificationGenerator.AddMockReturns();
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task> taskContinuation)
                taskContinuation.Returns(Task.CompletedTask);
            else if (Continuation is Moq.Language.Flow.ISetup<TService> voidContinuation)
                voidContinuation.Verifiable();
            else throw new NotImplementedException();
        }
    }

    private void SetupReturns(Func<TReturns> returns, string returnsExpr)
    {
        _spec.ArrangeLast(DoSetupReturns);

        void DoSetupReturns()
        {
            SpecifyMock();
            SpecificationGenerator.AddMockReturns(returnsExpr);
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
                asyncContinuation.ReturnsAsync(returns);
            else if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, TReturns> syncContinuation)
                syncContinuation.Returns(returns);
            else throw new NotImplementedException();
        }
    }

    private void SetupThrows<TException>()
        where TException : Exception, new()
    {
        _spec.ArrangeLast(DoSetupThrows);

        void DoSetupThrows()
        {
            SpecifyMock();
            SpecificationGenerator.AddMockThrows<TException>();
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, TReturns> returnsThrows)
                returnsThrows.Throws<TException>();
            else throw new NotImplementedException();
        }
    }

    private void SetupThrows(Func<Exception> expected, string expectedExpr)
    {
        _spec.ArrangeLast(DoSetupThrows);

        void DoSetupThrows()
        {
            SpecifyMock();
            SpecificationGenerator.AddMockThrows(expectedExpr);
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, TReturns> returnsThrows)
                returnsThrows.Throws(expected());
            else throw new NotImplementedException();
        }
    }

    private void SpecifyMock()
    {
        if (_callExpr is not null)
            SpecificationGenerator.AddMockSetup<TService>(_callExpr);
        if (_tapExpr is not null)
            SpecificationGenerator.AddTap(_tapExpr);
    }
}