using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Lazy<object> _lazyContinuation;
    protected readonly Func<bool, object> _setup;
    protected readonly string _callExpr;
    protected readonly string _tapExpr;
    protected bool IsSequential { get; set; }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, Func<bool, object> setup, string callExpr, string tapExpr = null)
    {
        _spec = spec;
        _setup = setup;
        _lazyContinuation = new Lazy<object>(DoSetup);
        _callExpr = callExpr;
        _tapExpr = tapExpr;
    }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec,
        Func<Mock<TService>, bool, object> setup,
        string callExpr = null)
        : this(spec, isSequential => setup(spec.GetMock<TService>(), isSequential), callExpr)
    {
    }

    private object DoSetup() => _setup(IsSequential);

    public IGivenThatReturnsContinuation<TSUT, TResult, TService, TReturns> Returns()
    {
        SetupReturns();
        return new GivenThatReturnsContinuation<TSUT, TResult, TService, TReturns>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService, TReturns> Returns(
        [NotNull] Func<TReturns> returns, [CallerArgumentExpression(nameof(returns))] string returnsExpr = null)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        SetupReturns(returns, returnsExpr);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService, TReturns>(_spec, this);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService, TReturns> ReturnsDefault()
        => Returns(() => default);

    public IGivenThatReturnsContinuation<TSUT, TResult, TService, TReturns> Throws<TException>()
        where TException : Exception, new()
    {
        SetupThrows<TException>();
        return new GivenThatReturnsContinuation<TSUT, TResult, TService, TReturns>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService, TReturns> Throws(
        Func<Exception> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SetupThrows(expected, expectedExpr);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService, TReturns>(_spec);
    }

    protected object Continuation => _lazyContinuation.Value;

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
            var mock = new Mock<IArgumentProvider>();
            Moq.Language.ISetupSequentialResult<Expression> setup = mock.SetupSequence(_ => _.GetArgument(1));
            SpecifyMock();
            SpecificationGenerator.AddMockReturns(returnsExpr);
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, TReturns> syncContinuation)
                syncContinuation.Returns(returns);
            else if(Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
                asyncContinuation.ReturnsAsync(returns);
            else if (Continuation is Moq.Language.ISetupSequentialResult<TReturns> sequentialContinuation)
                sequentialContinuation.Returns(returns);
            else if (Continuation is Moq.Language.ISetupSequentialResult<Task<TReturns>> sequentialAsyncContinuation)
                sequentialAsyncContinuation.ReturnsAsync(returns);
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
            else if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncReturnsThrows)
                asyncReturnsThrows.ThrowsAsync(expected());
            else if (Continuation is Moq.Language.ISetupSequentialResult<TReturns> sequentialContinuation)
                sequentialContinuation.Throws(expected());
            else if (Continuation is Moq.Language.ISetupSequentialResult<Task<TReturns>> sequentialAsyncContinuation)
                sequentialAsyncContinuation.ThrowsAsync(expected());
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