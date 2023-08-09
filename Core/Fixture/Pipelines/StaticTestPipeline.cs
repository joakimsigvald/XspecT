namespace XspecT.Fixture.Pipelines;

public abstract class StaticTestPipeline<TResult> : TestPipeline<TResult, StaticSpec<TResult>>
{
    protected StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }
}

public class StaticTestPipeline<TValue, TResult> : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> Given(TValue value)
    {
        Parent.Given(value);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> When(Action<TValue> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}

public class StaticTestPipeline<TValue1, TValue2, TResult>
    : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue1, TValue2, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue1, TValue2, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2)
    {
        Parent.Given(value1, value2);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}

public class StaticTestPipeline<TValue1, TValue2, TValue3, TResult>
    : StaticTestPipeline<TResult>, IStaticTestPipeline<TValue1, TValue2, TValue3, TResult>
{
    public StaticTestPipeline(StaticSpec<TResult> parent) : base(parent) { }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3)
    {
        Parent.Given(value1, value2, value3);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}