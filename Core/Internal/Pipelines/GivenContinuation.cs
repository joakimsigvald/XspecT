using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult, TService>(SubjectSpec<TSUT, TResult> subjectSpec) 
    : IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns>(subjectSpec, expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>(subjectSpec, expression);
}

internal class GivenContinuation<TSUT, TResult>(SubjectSpec<TSUT, TResult> subjectSpec) 
    : IGivenContinuation<TSUT, TResult> 
    where TSUT : class
{
    public IGivenSubjectTestPipeline<TSUT, TResult> That(Action setup) => subjectSpec.GivenSetup(setup);
}