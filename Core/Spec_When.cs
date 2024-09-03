using System.Runtime.CompilerServices;

namespace XspecT;

public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Action act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Action<TSUT> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
    Func<TSUT, TResult> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
    Func<TResult> act, [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Func<TSUT, Task> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Func<Task> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Func<TSUT, Task<TResult>> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <param name="actExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(
        Func<Task<TResult>> act,
        [CallerArgumentExpression(nameof(act))] string actExpr = null)
    {
        _pipeline.SetAction(act, actExpr);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <param name="tearDownExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(
        Action<TSUT> tearDown, [CallerArgumentExpression(nameof(tearDown))] string tearDownExpr = null)
    {
        _pipeline.SetTearDown(tearDown, tearDownExpr);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <param name="tearDownExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(
        Func<TSUT, Task> tearDown, [CallerArgumentExpression(nameof(tearDown))] string tearDownExpr = null)
    {
        _pipeline.SetTearDown(tearDown, tearDownExpr);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <param name="setUpExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(
        Action<TSUT> setUp, [CallerArgumentExpression(nameof(setUp))] string setUpExpr = null)
    {
        _pipeline.PrependSetUp(setUp, setUpExpr);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <param name="setUpExpr"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(
        Func<TSUT, Task> setUp, [CallerArgumentExpression(nameof(setUp))] string setUpExpr = null)
    {
        _pipeline.PrependSetUp(setUp, setUpExpr);
        return this;
    }
}