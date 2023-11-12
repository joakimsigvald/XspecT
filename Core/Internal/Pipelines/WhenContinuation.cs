namespace XspecT.Internal.Pipelines;

internal class WhenContinuation<TSUT, TResult>
    : SubjectTestPipelineBase<TSUT, TResult>, IWhenContinuation<TSUT, TResult>
    where TSUT : class
{
    internal WhenContinuation(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> And(Action<TSUT> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> And(Func<TSUT, TResult> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> And(Func<TSUT, Task> action)
        => Parent.When(action);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> And(Func<TSUT, Task<TResult>> func)
        => Parent.When(func);
}