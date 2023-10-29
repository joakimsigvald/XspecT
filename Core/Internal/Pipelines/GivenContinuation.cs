using System.Linq.Expressions;
using XspecT.Fixture;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult, TService> : IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;

    public GivenContinuation(SubjectSpec<TSUT, TResult> subjectSpec) => _subjectSpec = subjectSpec;

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);
}