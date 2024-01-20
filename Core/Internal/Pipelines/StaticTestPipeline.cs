namespace XspecT.Internal.Pipelines;

internal abstract class StaticTestPipeline<TResult> : TestPipeline<TResult, StaticSpec<TResult>>
{
    protected StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }
}

internal class StaticTestPipeline<TValue, TResult>
    : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue, TResult>
{
    internal StaticTestPipeline(StaticSpec<TResult> parent)
        : base(parent) { }

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
    internal StaticTestPipeline(StaticSpec<TResult> parent)
        : base(parent) { }

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
    internal StaticTestPipeline(StaticSpec<TResult> parent)
        : base(parent) { }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act)
        => Parent.When(act);

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act)
        => Parent.When(act);
}