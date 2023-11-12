namespace XspecT.Internal.Pipelines;

internal abstract class SubjectTestPipelineBase<TSUT, TResult>
    : TestPipeline<TResult, SubjectSpec<TSUT, TResult>>, ISubjectTestPipelineBase<TSUT, TResult>
    where TSUT : class
{
    internal SubjectTestPipelineBase(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
        => Parent.Given<TService>();

    public IGivenContinuation<TSUT, TResult> Given() => Parent.Given();

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
        => Parent.Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value) => Parent.Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value) => Parent.Given(value);
}