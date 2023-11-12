using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    private readonly SubjectSpec<TSUT, TResult> _subjectSpec;
    private readonly Expression<Func<TService, TReturns>> _expression;

    public GivenThatContinuation(
        SubjectSpec<TSUT, TResult> subjectSpec, Expression<Func<TService, TReturns>> expression)
    {
        _subjectSpec = subjectSpec;
        _expression = expression;
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns)
    {
        _subjectSpec.SetupMock(_expression, returns);
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