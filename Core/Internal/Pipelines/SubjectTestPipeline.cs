﻿namespace XspecT.Internal.Pipelines;

internal abstract class SubjectTestPipeline<TSUT, TResult>
    : SubjectTestPipelineBase<TSUT, TResult>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    internal SubjectTestPipeline(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> When(Action<TSUT> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> When(Func<TSUT, TResult> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> When(Func<TSUT, Task> action)
        => Parent.When(action);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IWhenContinuation<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
        => Parent.When(func);
}