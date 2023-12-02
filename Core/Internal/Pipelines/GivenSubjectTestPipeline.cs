namespace XspecT.Internal.Pipelines;

internal class GivenSubjectTestPipeline<TSUT, TResult>(SubjectSpec<TSUT, TResult> parent)
    : SubjectTestPipeline<TSUT, TResult>(parent), IGivenSubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class
        => Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Given(value);
    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value) => Given(value);
    public IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class => Given<TService>();
    public IGivenContinuation<TSUT, TResult> And() => Given();
}