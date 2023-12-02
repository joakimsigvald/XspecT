namespace XspecT.Internal.Pipelines;

internal abstract class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>(
    SubjectSpec<TSUT, TResult> subjectSpec)
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    protected readonly SubjectSpec<TSUT, TResult> Spec = subjectSpec;

    public IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns)
    {
        SetupReturns(returns);
        return new GivenSubjectTestPipeline<TSUT, TResult>(Spec);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Throws<TException>()
        where TException : Exception, new()
    {
        SetupThrows<TException>();
        return new GivenSubjectTestPipeline<TSUT, TResult>(Spec);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Throws(Func<Exception> ex)
    {
        SetupThrows(ex);
        return new GivenSubjectTestPipeline<TSUT, TResult>(Spec);
    }

    protected abstract void SetupReturns(Func<TReturns> returns);

    protected abstract void SetupThrows<TException>() where TException : Exception, new();

    protected abstract void SetupThrows(Func<Exception> ex);
}