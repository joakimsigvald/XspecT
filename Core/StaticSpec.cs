using XspecT.Internal.Pipelines;
using static XspecT.Internal.Pipelines.AsyncHelper;

namespace XspecT;

/// <summary>
/// Inherit to setup and run a test-pipeline for static methods
/// </summary>
public abstract class StaticSpec<TResult> : Spec<TResult>
{
    /// <summary>
    /// Instantiate the specification (test-class)
    /// </summary>
    protected StaticSpec() : base(new StaticPipeline<TResult>()) { }

    /// <summary>
    /// Provide the single-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="act"></param>
    /// <param name="v1"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue>(
        Action<TValue> act, TValue v1)
    {
        Pipeline.SetArguments(v1);
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the two-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="act"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2>(
        Action<TValue1, TValue2> act, TValue1 v1, TValue2 v2)
    {
        Pipeline.SetArguments((v1, v2));
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the three-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="act"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act, TValue1 v1, TValue2 v2, TValue3 v3)
    {
        Pipeline.SetArguments((v1, v2, v3));
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the single-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue>(
        Func<TValue, TResult> act, TValue v1)
    {
        Pipeline.SetArguments(v1);
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the two-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="act"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2>(
        Func<TValue1, TValue2, TResult> act, TValue1 v1, TValue2 v2)
    {
        Pipeline.SetArguments((v1, v2));
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the three-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="act"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act, TValue1 v1, TValue2 v2, TValue3 v3)
    {
        Pipeline.SetArguments((v1, v2, v3));
        Pipeline.When(act);
        return new StaticTestPipeline<TResult>(this);
    }

    /// <summary>
    /// Provide the single-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="action"></param>
    /// <param name="v1"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue>(
        Func<TValue, Task> action, TValue v1)
        => When(v => Execute(() => action(v)), v1);

    /// <summary>
    /// Provide the two-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="action"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2>(
        Func<TValue1, TValue2, Task> action, TValue1 v1, TValue2 v2)
        => When((a, b) => Execute(() => action(a, b)), v1, v2);

    /// <summary>
    /// Provide the three-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="action"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task> action, TValue1 v1, TValue2 v2, TValue3 v3)
        => When((a, b, c) => Execute(() => action(a, b, c)), v1, v2, v3);

    /// <summary>
    /// Provide the single-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="func"></param>
    /// <param name="v1"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue>(Func<TValue, Task<TResult>> func, TValue v1)
        => When(v => Execute(() => func(v)), v1);

    /// <summary>
    /// Provide the two-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="func"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2>(
        Func<TValue1, TValue2, Task<TResult>> func, TValue1 v1, TValue2 v2)
        => When((a, b) => Execute(() => func(a, b)), v1, v2);

    /// <summary>
    /// Provide the three-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="func"></param>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="v3"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task<TResult>> func, TValue1 v1, TValue2 v2, TValue3 v3)
        => When((a, b, c) => Execute(() => func(a, b, c)), v1, v2, v3);

    private StaticPipeline<TResult> Pipeline => (StaticPipeline<TResult>)_pipeline;
}