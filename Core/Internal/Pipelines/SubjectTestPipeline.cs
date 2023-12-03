namespace XspecT.Internal.Pipelines;

internal abstract class SubjectTestPipeline<TSUT, TResult>
    : TestPipeline<TResult, SubjectSpec<TSUT, TResult>>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    internal SubjectTestPipeline(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
        => Parent.When(act);

    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
        => Parent.When(act);

    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
        => Parent.When(action);

    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
        => Parent.When(func);

    public ISubjectTestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
        => Parent.After(setUp);

    public ISubjectTestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
        => Parent.After(setUp);

    public ISubjectTestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
        => Parent.Before(tearDown);

    public ISubjectTestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
        => Parent.Before(tearDown);

    public IGivenSubjectTestPipeline<TSUT, TResult> GivenDefault<TValue>(Action<TValue> setup) where TValue : class
        => Parent.GivenDefault(setup);

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
        => Parent.Given<TService>();

    public IGivenContinuation<TSUT, TResult> Given() => Parent.Given();

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
        => Parent.Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value) => Parent.Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value) => Parent.Given(value);

}