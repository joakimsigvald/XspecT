using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult, TService> : IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;

    internal GivenContinuation(SubjectSpec<TSUT, TResult> subjectSpec) 
        => _subjectSpec = subjectSpec;

    public IGivenSubjectTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value)
    {
        _subjectSpec.SetupMock<TService>(mock => mock.SetReturnsDefault(value()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(_subjectSpec);
    }

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);
}

internal class GivenContinuation<TSUT, TResult>
    : IGivenContinuation<TSUT, TResult> 
    where TSUT : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;

    internal GivenContinuation(SubjectSpec<TSUT, TResult> subjectSpec)
        => _subjectSpec = subjectSpec;

    public IGivenSubjectTestPipeline<TSUT, TResult> That(Action setup) => _subjectSpec.GivenSetup(setup);
}