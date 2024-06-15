using System.Globalization;
using XspecT.Internal.Pipelines;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        _pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        _pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        _pipeline.SetAction(action);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        _pipeline.SetAction(func);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
    {
        _pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
    {
        _pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
    {
        _pipeline.PrependSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
    {
        _pipeline.PrependSetUp(setUp);
        return this;
    }
}