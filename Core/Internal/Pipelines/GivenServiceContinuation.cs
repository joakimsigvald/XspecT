using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenServiceContinuation<TSUT, TResult, TService> : IGivenServiceContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenServiceContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TReturns>(
        Func<TReturns> returns,
        [CallerArgumentExpression(nameof(returns))] string returnsExpr = null)
    {
        var theValue = returns();
        _spec.ArrangeLast(DoSetupReturnsDefault);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);

        void DoSetupReturnsDefault()
        {
            _spec.GetMock<TService>().SetReturnsDefault(theValue);
            _spec.GetMock<TService>().SetReturnsDefault(Task.FromResult(theValue));
            SpecificationGenerator.AddMockReturnsDefault<TService>(returnsExpr);
        }
    }

    public IGivenTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new()
    {
        SpecificationGenerator.AddMockThrowsDefault<TService, TException>();
        _spec.SetupThrows<TService>(_spec.Another<TException>);
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws(
        Func<Exception> expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.AddMockThrowsDefault<TService>(expectedExpr);
        _spec.SetupThrows<TService>(expected);
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, TReturns>(_spec, call, callExpr);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, Task<TReturns>>(_spec, call, callExpr);
}