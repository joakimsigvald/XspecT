namespace XspecT.Internal.Pipelines;

internal abstract class StaticTestPipeline<TResult> : TestPipeline<TResult, StaticSpec<TResult>>
{
    protected StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }
}

internal class StaticTestPipeline<TValue, TResult> : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue, TResult> Given(TValue value)
        => Parent.Given(value);

    public IStaticTestPipeline<TValue, TResult> When(Action<TValue> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, TResult> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act)
        => Parent.When(act);
}

internal class StaticTestPipeline<TValue1, TValue2, TResult>
    : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue1, TValue2, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2)
        => Parent.Given(value1, value2);

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act)
        => Parent.When(act);
}

internal class StaticTestPipeline<TValue1, TValue2, TValue3, TResult>
    : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue1, TValue2, TValue3, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3)
        => Parent.Given(value1, value2, value3);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act)
        => Parent.When(act);
}