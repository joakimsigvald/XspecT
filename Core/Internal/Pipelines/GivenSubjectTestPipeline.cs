using XspecT.Fixture;

namespace XspecT.Internal.Pipelines;

internal class GivenSubjectTestPipeline<TSUT, TResult>
    : SubjectTestPipeline<TSUT, TResult>, IGivenSubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    public GivenSubjectTestPipeline(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class
        => Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Given(value);
    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value) => Given(value);

    public IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class
        => Given<TService>();
}