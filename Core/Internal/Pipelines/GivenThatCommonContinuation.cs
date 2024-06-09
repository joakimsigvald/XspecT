namespace XspecT.Internal.Pipelines;

internal abstract class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    internal GivenThatCommonContinuation(Spec<TSUT, TResult> subjectSpec) 
        => Spec = subjectSpec;

    protected readonly Spec<TSUT, TResult> Spec;

    public IGivenTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns)
    {
        SetupReturns(returns);
        return new GivenTestPipeline<TSUT, TResult>(Spec);
    }

    public IGivenTestPipeline<TSUT, TResult> ReturnsDefault()
    {
        SetupReturns(() => default);
        return new GivenTestPipeline<TSUT, TResult>(Spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws<TException>()
        where TException : Exception, new()
    {
        SetupThrows<TException>();
        return new GivenTestPipeline<TSUT, TResult>(Spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws(Func<Exception> ex)
    {
        SetupThrows(ex);
        return new GivenTestPipeline<TSUT, TResult>(Spec);
    }

    protected abstract void SetupReturns(Func<TReturns> returns);

    protected abstract void SetupThrows<TException>() where TException : Exception, new();

    protected abstract void SetupThrows(Func<Exception> ex);
}