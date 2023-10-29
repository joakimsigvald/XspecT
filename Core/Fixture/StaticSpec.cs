using XspecT.Internal.Pipelines;
using static XspecT.Internal.Pipelines.AsyncHelper;

namespace XspecT.Fixture;

public abstract class StaticSpec<TResult> : Spec<TResult>
{
    protected StaticSpec() : base(new StaticPipeline<TResult>()) { }

    private StaticPipeline<TResult> Pipeline => (StaticPipeline<TResult>)_pipeline;

    public IStaticTestPipeline<TValue, TResult> Given<TValue>(TValue value)
    {
        Pipeline.SetArguments(value);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2)
    {
        Pipeline.SetArguments((value1, value2));
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3)
    {
        Pipeline.SetArguments((value1, value2, value3));
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Action<TValue> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TResult>(this);
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act)
    {
        Pipeline.When(act);
        return new StaticTestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task> action)
        => When<TValue>(v => Execute(() => action(v)));

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task> action)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => action(v1, v2)));

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task> action)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => action(v1, v2, v3)));

    public IStaticTestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task<TResult>> func)
        => When<TValue>(v => Execute(() => func(v)));

    public IStaticTestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task<TResult>> func)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => func(v1, v2)));

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task<TResult>> func)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => func(v1, v2, v3)));
}