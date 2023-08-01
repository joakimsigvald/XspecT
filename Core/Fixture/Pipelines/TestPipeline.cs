using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;


public class TestPipeline<TResult>
{
    protected readonly SpecBase<TResult> Parent;
    protected TestPipeline(SpecBase<TResult> parent) => Parent = parent;
    public TestResult<TResult> Then => Parent.Then;
}

public class TestPipeline<TValue, TResult> : TestPipeline<TResult>, ITestPipeline<TValue, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public ITestPipeline<TValue, TResult> Given(TValue value)
    {
        Parent.Given(value);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Action<TValue> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Func<TValue, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Func<TValue, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}

public class TestPipeline<TValue1, TValue2, TResult>
    : TestPipeline<TResult>, ITestPipeline<TValue1, TValue2, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue1, TValue2, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2)
    {
        Parent.Given(value1, value2);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}

public class TestPipeline<TValue1, TValue2, TValue3, TResult>
    : TestPipeline<TResult>, ITestPipeline<TValue1, TValue2, TValue3, TResult>
{
    public TestPipeline(SpecBase<TResult> parent) : base(parent) { }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> GivenThat(Action arrangement)
    {
        Parent.GivenThat(arrangement);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3)
    {
        Parent.Given(value1, value2, value3);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Using<TService>(TService service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Using<TService>(Func<TService> service)
    {
        Parent.Using(service);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act)
    {
        Parent.When(act);
        return this;
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act)
    {
        Parent.When(act);
        return this;
    }
}