using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>(
    SubjectSpec<TSUT, TResult> subjectSpec, Expression<Func<TService, Task<TReturns>>> expression)
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>(subjectSpec)
    where TSUT : class
    where TService : class
{
    protected override void SetupReturns(Func<TReturns> returns)
        => Spec.SetupMock(expression, returns);

    protected override void SetupThrows<TException>()
        => Spec.SetupMock<TService>(_ => _.Setup(expression).Throws<TException>());

    protected override void SetupThrows(Func<Exception> ex)
        => Spec.SetupMock<TService>(_ => _.Setup(expression).Throws(ex()));
}