using Moq;
using System.Linq.Expressions;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public class GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;
    private readonly Expression<Func<TService, Task<TReturns>>> _expression;

    public GivenThatAsyncContinuation(
        SubjectSpec<TSUT, TResult> subjectSpec, Expression<Func<TService, Task<TReturns>>> expression)
    {
        _subjectSpec = subjectSpec;
        _expression = expression;
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns)
    {
        _subjectSpec.SetupMock<TService>(_ => _.Setup(_expression).ReturnsAsync(returns()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(_subjectSpec);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Throws<TException>()
        where TException : Exception, new()
    {
        _subjectSpec.SetupMock<TService>(_ => _.Setup(_expression).Throws<TException>());
        return new GivenSubjectTestPipeline<TSUT, TResult>(_subjectSpec);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Throws(Func<Exception> ex)
    {
        _subjectSpec.SetupMock<TService>(_ => _.Setup(_expression).Throws(ex()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(_subjectSpec);
    }
}