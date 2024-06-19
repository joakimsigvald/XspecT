using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    private readonly Moq.Language.Flow.ISetup<TService, TReturns> _setup;
    private readonly Expression<Func<TService, TReturns>> _expression;

    internal GivenThatContinuation(
        Spec<TSUT, TResult> subjectSpec, Expression<Func<TService, TReturns>> expression)
        : base(subjectSpec) => _setup = Spec.GetMock<TService>().Setup(expression);

    protected override void SetupReturns(Func<TReturns> returns)
        => Spec.GivenSetup(() => _setup.Returns(returns));

    protected override void SetupThrows<TException>()
        => Spec.GivenSetup(() => _setup.Throws<TException>());

    protected override void SetupThrows(Func<Exception> ex)
        => Spec.GivenSetup(() => _setup.Throws(ex()));
}