using Moq;
using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    private readonly Moq.Language.Flow.ISetup<TService, Task<TReturns>> _setup;

    internal GivenThatAsyncContinuation(
        Spec<TSUT, TResult> subjectSpec, Expression<Func<TService, Task<TReturns>>> expression)
        : base(subjectSpec) => _setup = Spec.GetMock<TService>().Setup(expression);

    protected override void SetupReturns(Func<TReturns> returns)
        => Spec.GivenSetup(() => _setup.ReturnsAsync(returns));

    protected override void SetupThrows<TException>()
        => Spec.GivenSetup(() => _setup.Throws<TException>());

    protected override void SetupThrows(Func<Exception> ex)
        => Spec.GivenSetup(() => _setup.Throws(ex()));
}