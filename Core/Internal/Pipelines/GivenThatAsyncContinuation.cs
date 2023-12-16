using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    private readonly Expression<Func<TService, Task<TReturns>>> _expression;

    internal GivenThatAsyncContinuation(
        SubjectSpec<TSUT, TResult> subjectSpec, Expression<Func<TService, Task<TReturns>>> expression)
        : base(subjectSpec) => _expression = expression;

    protected override void SetupReturns(Func<TReturns> returns)
        => Spec.SetupMock(_expression, returns);

    protected override void SetupThrows<TException>()
        => Spec.SetupMock<TService>(_ => _.Setup(_expression).Throws<TException>());

    protected override void SetupThrows(Func<Exception> ex)
        => Spec.SetupMock<TService>(_ => _.Setup(_expression).Throws(ex()));
}