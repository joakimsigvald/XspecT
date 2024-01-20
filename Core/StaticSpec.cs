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
    /// Provide the argument for a single argument static method to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue, TResult> Given<TValue>(TValue value)
    {
        Pipeline.SetArguments(value);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    /// <summary>
    /// Provide arguments for a two-argument static method to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2)
    {
        Pipeline.SetArguments((value1, value2));
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    /// <summary>
    /// Provide arguments for a three-argument static method to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="value3"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3)
    {
        Pipeline.SetArguments((value1, value2, value3));
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    /// <summary>
    /// Provide the single-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue, TResult> When<TValue>(Action<TValue> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    /// <summary>
    /// Provide the two-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    /// <summary>
    /// Provide the three-argument static action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    /// <summary>
    /// Provide the single-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    /// <summary>
    /// Provide the two-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    /// <summary>
    /// Provide the three-argument static function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="act"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    /// <summary>
    /// Provide the single-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task> action)
        => When<TValue>(v => Execute(() => action(v)));

    /// <summary>
    /// Provide the two-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task> action)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => action(v1, v2)));

    /// <summary>
    /// Provide the three-argument static async action to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task> action)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => action(v1, v2, v3)));

    /// <summary>
    /// Provide the single-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task<TResult>> func)
        => When<TValue>(v => Execute(() => func(v)));

    /// <summary>
    /// Provide the two-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task<TResult>> func)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => func(v1, v2)));

    /// <summary>
    /// Provide the three-argument static async function to be tested
    /// </summary>
    /// <typeparam name="TValue1"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TValue3"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task<TResult>> func)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => func(v1, v2, v3)));

    private StaticPipeline<TResult> Pipeline => (StaticPipeline<TResult>)_pipeline;
}